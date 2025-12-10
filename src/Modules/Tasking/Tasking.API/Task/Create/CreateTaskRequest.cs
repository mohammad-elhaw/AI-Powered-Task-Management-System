using Tasking.API.TaskComment.Create;
using Tasking.API.TaskItem.Create;

namespace Tasking.API.Task.Create;

public record CreateTaskRequest
{
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Priority { get; init; } = default!;
    public string Status { get; init; } = default!;
    public DateTime? DueDate { get; init; }

    public List<CreateTaskItemRequest> Items { get; init; } = [];
    public List<CreateCommentRequest> Comments { get; init; } = [];
}
