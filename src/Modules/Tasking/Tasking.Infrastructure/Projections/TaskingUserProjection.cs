namespace Tasking.Infrastructure.Projections;

public class TaskingUserProjection
{
    public Guid UserId { get; private set; }
    public string DisplayName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public TaskingUserProjection() { }

    public TaskingUserProjection(
        Guid userId, 
        string displayName, 
        string email, 
        bool isActive)
    {
        UserId = userId;
        DisplayName = displayName;
        Email = email;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void UpdateProfile(string displayName, string email)
    {
        DisplayName = displayName;
        Email = email;
    }
}
