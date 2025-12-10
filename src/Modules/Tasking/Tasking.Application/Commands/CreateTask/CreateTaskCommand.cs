using Shared.Application.Abstractions.CQRS;

namespace Tasking.Application.Commands.CreateTask;

public record CreateTaskCommand(
    string Title,
    string Description,
    string Priority,
    string Status,
    DateTime? DueDate,
    List<CreateTaskItemModel> Items,
    List<CreateCommentModel> Comments) 
    : ICommand<CreateTaskResult>;

public record CreateTaskItemModel(string Content, bool IsCompleted);
public record CreateCommentModel(string Content, string Author);