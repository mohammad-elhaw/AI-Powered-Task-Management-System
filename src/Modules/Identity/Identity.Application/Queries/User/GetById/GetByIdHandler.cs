using Identity.Application.Dtos;
using Identity.Application.Errors;
using Identity.Domain.Repositories;
using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;

namespace Identity.Application.Queries.User.GetById;

public class GetByIdHandler(
    IUserRepository userRepo)
    : IQueryHandler<GetByIdQuery, GetByIdResult>
{
    public async Task<Result<GetByIdResult>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepo.GetById(query.UserId, cancellationToken);

            if (user is null)
                return Result<GetByIdResult>.Failure(UserErrors.UserNotFound);

            var roleNames = user.UserRoles
                .Select(u => u.Role.Name)
                .ToList();

            return Result<GetByIdResult>.Success(new GetByIdResult(new UserDto(
                user.Id,
                user.KeycloakId,
                user.Email.Value,
                user.Name.FirstName,
                user.Name.LastName,
                user.IsActive,
                roleNames)));
        }
        catch (Exception ex)
        {
            return Result<GetByIdResult>.Failure(
                new Error(
                    "GetUserById.Error", 
                    "UnexpectedError", 
                    ex.Message));
        }
    }
}
