using Identity.Domain.Entities;
using Identity.Domain.Exceptions.Role;
using Shared.Domain.Abstractions;

namespace Identity.Domain.Aggregates;

public class Role : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    private readonly List<Permission> _permissions = [];
    public IReadOnlyList<Permission> Permissions => _permissions.AsReadOnly();

    private Role() { }

    private Role(Guid id, string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new InvalidRoleNameException();

        Id = id;
        Name = name;
    }

    public static Role Create(Guid id, string name)
        => new(id, name);

    public void AddPermission(Permission permission)
    {
        if (!_permissions.Any(p => p.Id == permission.Id))
            _permissions.Add(permission);
    }

}
