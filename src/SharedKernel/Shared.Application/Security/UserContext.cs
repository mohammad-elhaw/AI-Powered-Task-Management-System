namespace Shared.Application.Security;

public class UserContext(string keycloakUserId, IReadOnlySet<string> permissions)
{
    public string KeycloakUserId { get; } = keycloakUserId;
    public IReadOnlySet<string> Permissions { get; } = permissions;

    public bool HasPermission(string permission) => Permissions.Contains(permission);
}
