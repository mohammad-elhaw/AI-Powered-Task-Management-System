using Tasking.Application.Tasks.TaskComments;
using Tasking.Application.Tasks.TaskItems.AddTaskItem;

namespace Tasking.API.Task.Create;

public record CreateTaskResponse(TaskDto TaskDto);
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
