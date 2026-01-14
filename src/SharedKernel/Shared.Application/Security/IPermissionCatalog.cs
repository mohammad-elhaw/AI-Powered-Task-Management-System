namespace Shared.Application.Security;

public interface IPermissionCatalog
{
    IReadOnlySet<string> AllPermissions();
}
