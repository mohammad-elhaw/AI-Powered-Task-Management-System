using Identity.Application.Abstractions.Permissions;
using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Queries.User.Permissions;

public class GetUserPermissionsHandler
    (IUserPermissionsRepository userPermissionsRepository)
    : IQueryHandler<GetUserPermissionsQuery, IReadOnlySet<string>>
{
    public async Task<IReadOnlySet<string>> Handle(GetUserPermissionsQuery query, CancellationToken cancellationToken)
    {
        var userPermissions = await userPermissionsRepository.GetUserPermissions(query.KeycloakId, cancellationToken);
        return userPermissions;
    }
}
