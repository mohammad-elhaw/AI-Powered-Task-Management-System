using Identity.Domain.Aggregates;

namespace Identity.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public User User { get; private set; } = null!;
    public Role Role { get; private set; } = null!;

    private UserRole() { }

    public UserRole(User user, Role role)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Role = role ?? throw new ArgumentNullException(nameof(role));
        UserId = user.Id;
        RoleId = role.Id;
    }
}
