namespace Tasking.Application.Tasks.TaskItmes.AddTaskItem;

public record AddTaskItemResult(TaskItemDto TaskItemDto);

public record TaskItemDto(int Id, string Content, bool IsCompleted);