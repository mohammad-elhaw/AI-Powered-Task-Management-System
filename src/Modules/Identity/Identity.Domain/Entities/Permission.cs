using Identity.Domain.Exceptions.Permission;
using Shared.Domain.Abstractions;

namespace Identity.Domain.Entities;

public class Permission : Entity<int>
{
    public string Name { get; private set; }
    private Permission() { }
    public Permission(int id, string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new InvalidPermissionNameException();

        Id = id;
        Name = name;
    }
}
