using Shared.Application.Abstractions.CQRS;

namespace Tasking.Application.Tasks.TaskItems.AddTaskItem;

public record AddTaskItemCommand(Guid TaskId, string Content) : ICommand<AddTaskItemResult>;