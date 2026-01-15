namespace Tasking.Application.Tasks.GetTasks;

public record GetAllTasksResult(IReadOnlyList<TaskDto> TasksDto);

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

public record TaskItemDto(int Id, string Content, bool IsCompleted);
public record CommentDto(int Id, string Content, string Author);