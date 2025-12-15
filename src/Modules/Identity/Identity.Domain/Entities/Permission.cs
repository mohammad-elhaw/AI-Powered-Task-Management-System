using Shared.Domain.Abstractions;

namespace Identity.Domain.Entities;

public class Permission : Entity<int>
{
    public string Name { get; private set; }
    private Permission() { }
    public Permission(int id, string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Permission name cannot be null or empty.", nameof(name));

        Id = id;
        Name = name;
    }
}
