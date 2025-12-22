using Identity.Application.Dtos;
using Shared.Application.Results;

namespace Identity.Application.Abstractions;

public interface IIdentityProvider
{
    Task<Result<string>> CreateUser(string userName, string email, string firstName,
        string lastName, string tempPassword, CancellationToken cancellationToken);
    Task DeleteUser(string keyCloakUserId, CancellationToken cancellationToken);
    Task<IReadOnlyList<UserDto>> GetAllUsers(CancellationToken cancellationToken);
    Task AssignRole(string keyCloakUserId, string roleName, CancellationToken cancellationToken);
    Task EnsureRealmRoleExists(string roleName, CancellationToken cancellationToken);
    Task<List<string>> GetUserRoles(string keycloakUserId, CancellationToken cancellationToken);
}
