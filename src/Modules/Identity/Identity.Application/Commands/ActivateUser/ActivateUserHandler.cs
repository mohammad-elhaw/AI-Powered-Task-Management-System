using Identity.Application.Abstractions.IdentityProvider;
using Identity.Domain.Repositories;
using MediatR;
using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;

namespace Identity.Application.Commands.ActivateUser;

internal class ActivateUserHandler(
    IUserRepository userRepository,
    IIdentityProvider identityProvider)
    : ICommandHandler<ActivateUserCommand>
{
    public async Task<Result<Unit>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.GetById(request.UserId, cancellationToken);
            if (user is null)
            {
                return Result<Unit>.Failure(
                    new Error(
                        "ActivateUser.UserNotFound",
                        "UserNotFound",
                        $"User with ID {request.UserId} was not found."));
            }
            await identityProvider.Activate(user.KeycloakId, cancellationToken);
            user.Activate();
            await userRepository.SaveChanges(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(
                new Error(
                    "DeactivateUser.Error",
                    "DeactivateUserError",
                    ex.Message));
        }
    }
}
