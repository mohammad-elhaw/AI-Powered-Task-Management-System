using Identity.Application.Queries.User.Permissions;
using MediatR;
using Shared.Application.Security;

namespace Identity.Infrastructure.Services;

public class UserPermissionService(
    IMediator mediator)
    : IUserPermissionService
{
    public async Task<IReadOnlySet<string>> GetUserPermissions(string userKeycloakId, CancellationToken cancellationToken = default)
    {
        var userPermissions = await mediator.Send(new GetUserPermissionsQuery(userKeycloakId), cancellationToken);
        return userPermissions;
    }
}
