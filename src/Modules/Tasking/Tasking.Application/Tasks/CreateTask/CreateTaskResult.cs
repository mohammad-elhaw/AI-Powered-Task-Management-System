using Tasking.Application.Tasks.Comments;
using Tasking.Application.Tasks.TaskItmes.AddTaskItem;

namespace Tasking.Application.Tasks.CreateTask;

public record CreateTaskResult(TaskDto TaskDto);
public record TaskDto(
    Guid Id,
    string Title,
    string Description,
    string Priority,
    string Status,
    DateTime? DueDate,
    IReadOnlyList<TaskItemDto> Items,
    IReadOnlyList<CommentDto> Comments
);