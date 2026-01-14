using Shared.Application.Security;

namespace Tasking.Application.Tasks.Security;

public class TaskPermissionCatalog : IPermissionCatalog
{
    public IReadOnlySet<string> AllPermissions()
        => new HashSet<string>
        {
            TaskPermissions.Create,
            TaskPermissions.Update,
            TaskPermissions.Delete,
            TaskPermissions.Assign,
            TaskPermissions.View
        };
}
