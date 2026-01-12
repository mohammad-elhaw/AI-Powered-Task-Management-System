using Identity.Domain.Exceptions.Permission;
using Shared.Domain.Abstractions;

namespace Identity.Domain.Entities;

public class Permission : Entity<int>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    private Permission() { }
    public Permission(string name, string code)
    {
        if (string.IsNullOrEmpty(name))
            throw new InvalidPermissionNameException();

        if (string.IsNullOrEmpty(code))
            throw new InvalidPermissionCodeException();

        Name = name;
        Code = code;
    }
}
