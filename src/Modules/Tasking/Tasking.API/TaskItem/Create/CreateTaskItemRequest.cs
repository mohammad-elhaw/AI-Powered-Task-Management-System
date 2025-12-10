namespace Tasking.API.TaskItem.Create;

public record CreateTaskItemRequest
{
    public string? Content { get; init; }
    public bool IsCompleted { get; init; } = false;
}
