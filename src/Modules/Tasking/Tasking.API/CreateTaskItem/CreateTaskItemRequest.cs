namespace Tasking.API.CreateTaskItem;

public record CreateTaskItemRequest
{
    public string? Content { get; init; }
    public bool IsCompleted { get; init; } = false;
}
