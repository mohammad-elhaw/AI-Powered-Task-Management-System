namespace Tasking.Domain.ReadModels;

public class TaskingUserRole
{
    public Guid UserId { get; set; }
    public string RoleName { get; set; } = default!;
}
