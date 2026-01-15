using Tasking.API.CreateTaskComment;
using Tasking.API.CreateTaskItem;

namespace Tasking.API.CreateTask;

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
