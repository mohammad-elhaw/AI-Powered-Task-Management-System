using Shared.Application.Results;

namespace Shared.Application.Security;

public interface IUserPermissionService
{
    Task<Result<IReadOnlySet<string>>> GetUserPermissions(string userKeycloakId, CancellationToken cancellationToken = default);
}