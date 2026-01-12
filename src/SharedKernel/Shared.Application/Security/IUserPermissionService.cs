namespace Shared.Application.Security;

public interface IUserPermissionService
{
    Task<IReadOnlySet<string>> GetUserPermissions(string userKeycloakId, CancellationToken cancellationToken = default);
}