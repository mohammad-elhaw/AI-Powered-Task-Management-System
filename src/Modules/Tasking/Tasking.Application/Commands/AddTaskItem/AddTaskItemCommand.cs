using Shared.Application.Abstractions.CQRS;

namespace Tasking.Application.Commands.AddTaskItem;

public record AddTaskItemCommand(Guid TaskId, string Content) : ICommand<AddTaskItemResult>;