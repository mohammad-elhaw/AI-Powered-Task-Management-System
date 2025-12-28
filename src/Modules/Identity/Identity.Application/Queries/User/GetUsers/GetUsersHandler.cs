using Identity.Application.Abstractions;
using Identity.Application.Abstractions.IdentityProvider;
using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Queries.User.GetUsers;

public class GetUsersHandler(
    IIdentityProvider identityProvider,
    IUserReadRepository userReadRepository)
    : IQueryHandler<GetUsersQuery, GetUsersResult>
{
    public async Task<GetUsersResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var response = await identityProvider.GetAllUsers(cancellationToken);

        var keycloakIds = response.Users.Select(u => u.KeycloakId).ToList();

        var rolesDic = await userReadRepository
            .GetRolesByKeycloakIds(keycloakIds, cancellationToken);

        var dbUsersDic = await userReadRepository
            .GetUserIdsByKeycloakIds(keycloakIds, cancellationToken);



        var usersToReturn =  response.Users.Select(u =>
        new UserDto(
            Id: dbUsersDic.TryGetValue(u.KeycloakId, out Guid id)
                ? id : Guid.Empty,
            KeycloakId: u.KeycloakId,
            Email: u.Email,
            FirstName: u.FirstName,
            LastName: u.LastName,
            u.IsActive,
            RoleNames: rolesDic.TryGetValue(u.KeycloakId, out var roles)
                ? roles : [])).ToList();

        return new GetUsersResult(usersToReturn);
    }
}
