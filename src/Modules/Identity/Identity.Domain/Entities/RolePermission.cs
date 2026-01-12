using Identity.Domain.Aggregates;

namespace Identity.Domain.Entities;

public class RolePermission
{
    public Guid RoleId { get; private set; }
    public int PermissionId { get; private set; }

    public Role Role { get; private set; } = null!;
    public Permission Permission { get; private set; } = null!;
    
    private RolePermission() { }
    public RolePermission(Role role, Permission permission)
    {
        Role = role;
        Permission = permission;
        RoleId = role.Id;
        PermissionId = permission.Id;
    }
}
