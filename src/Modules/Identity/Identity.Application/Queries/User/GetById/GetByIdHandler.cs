using Identity.Application.Dtos;
using Identity.Domain.Repositories;
using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Queries.User.GetById;

public class GetByIdHandler(
    IUserRepository userRepo)
    : IQueryHandler<GetByIdQuery, GetByIdResult>
{
    public async Task<GetByIdResult> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetById(query.UserId, cancellationToken) 
            ?? throw new ArgumentNullException(nameof(query), "User Not found");
        
        var roleNames = user.UserRoles
            .Select(u => u.Role.Name)
            .ToList();
        
        return new GetByIdResult(new UserDto(
            user.Id,
            user.KeycloakId,
            user.Email.Value,
            user.Name.FirstName,
            user.Name.LastName,
            user.IsActive,
            roleNames));

    }
}
