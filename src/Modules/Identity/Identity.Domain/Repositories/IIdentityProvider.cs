namespace Identity.Domain.Repositories;

public interface IIdentityProvider
{
    Task<string> CreateUser(string userName, string email, string firstName, 
        string lastName, string tempPassword, CancellationToken cancellationToken);
    Task DeleteUser(string keyCloakUserId, CancellationToken cancellationToken);
    Task<List<object>> GetAllUsers(CancellationToken cancellationToken);
    Task AssignRole(string keyCloakUserId, string roleName, CancellationToken cancellationToken);
    Task EnsureRealmRoleExists(string roleName, CancellationToken cancellationToken);
}
