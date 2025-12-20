using Identity.Application.Abstractions;
using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Queries.User.GetUsers;

public class GetUsersHandler(
    IIdentityProvider identityProvider,
    IUserReadRepository userReadRepository)
    : IQueryHandler<GetUsersQuery, GetUsersResult>
{
    public async Task<GetUsersResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await identityProvider.GetAllUsers(cancellationToken);

        var keycloakIds = users.Select(u => u.KeycloakId).ToList();

        var rolesDic = await userReadRepository
            .GetRolesByKeycloakIds(keycloakIds, cancellationToken);

        var dbUsersDic = await userReadRepository
            .GetUserIdsByKeycloakIds(keycloakIds, cancellationToken);



        var usersToReturn = users.Select(u => u with
        {
            Id = dbUsersDic.TryGetValue(u.KeycloakId, out Guid id) 
                ? id : Guid.Empty,
            RoleNames = rolesDic.TryGetValue(u.KeycloakId, out var roles) 
                ? roles : new List<string>()
        }).ToList();

        return new GetUsersResult(usersToReturn);
    }
}
