namespace Identity.Application.Abstractions.Permissions;

public interface IUserPermissionsRepository
{
    Task<IReadOnlySet<string>> GetUserPermissions(string keyCloakId, CancellationToken cancellationToken); 
}
