using Identity.Application.Abstractions.IdentityProvider;
using Identity.Application.Errors;
using Identity.Domain.Repositories;
using MediatR;
using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;

namespace Identity.Application.Commands.DeleteUser;

public class DeleteUserHandler(
    IIdentityProvider identityProvider,
    IUserRepository userRepository)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result<Unit>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.GetById(command.UserId, cancellationToken);
        
            if(user is null)
                return Result<Unit>.Failure(UserErrors.UserNotFound);

            await identityProvider.DeleteUser(user.KeycloakId, cancellationToken);
            await userRepository.Delete(user, cancellationToken);
            await userRepository.SaveChanges(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(new Error(
                "UserDelete.Error",
                "DeleteUserError", 
                ex.Message));
        }
    }
}
