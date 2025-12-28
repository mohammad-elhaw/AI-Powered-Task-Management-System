using Identity.Application.Abstractions.IdentityProvider.GetAllUsers;
using Shared.Application.Results;

namespace Identity.Application.Abstractions.IdentityProvider;

public interface IIdentityProvider
{
    Task<Result<string>> CreateUser(CreateUser.Request request, CancellationToken cancellationToken);
    Task DeleteUser(string keyCloakUserId, CancellationToken cancellationToken);
    Task<Response> GetAllUsers(CancellationToken cancellationToken);
    Task AssignRole(string keyCloakUserId, string roleName, CancellationToken cancellationToken);
    Task EnsureRealmRoleExists(string roleName, CancellationToken cancellationToken);
    Task<List<string>> GetUserRoles(string keycloakUserId, CancellationToken cancellationToken);
}
