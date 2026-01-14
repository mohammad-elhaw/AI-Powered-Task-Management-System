using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;
using Shared.Application.Security;
using Tasking.Application.Tasks.Comments;
using Tasking.Application.Tasks.Security.Authorization.CreateTask;
using Tasking.Application.Tasks.TaskItmes.AddTaskItem;
using Tasking.Domain.Enums;
using Tasking.Domain.Repositories;
using Tasking.Domain.ValueObjects;

namespace Tasking.Application.Tasks.CreateTask;

internal class CreateTaskHandler(
    ITaskRepository taskRepository,
    ICreateTaskPolicy createTaskPolicy,
    IUserContextProvider userContext)
    : ICommandHandler<CreateTaskCommand, CreateTaskResult>
{
    public async Task<Result<CreateTaskResult>> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var user = await userContext.GetAsync();

        if (user.IsFailure)
            return Result<CreateTaskResult>.Failure(user.Error);

        var policyResult = createTaskPolicy.Check(user.Value!);

        if (policyResult.IsFailure)
            return Result<CreateTaskResult>.Failure(policyResult.Error);

        var task = Domain.Aggregates.Task.Create(
            title: TaskTitle.Create(command.Title),
            description: TaskDescription.Create(command.Description),
            priority: Enum.Parse<Priority>(command.Priority),
            dueDate: command.DueDate.HasValue
                ? DueDate.Create(command.DueDate.Value.ToUniversalTime())
                : null!
        );
        foreach (var itemDto in command.Items)
        {
            task.AddItem(itemDto.Content);
        }

        foreach (var commentDto in command.Comments)
        {
            task.AddComment(commentDto.Content, commentDto.Author);
        }

        await taskRepository.AddTask(task, cancellationToken);
        await taskRepository.SaveChanges(cancellationToken);
        var resultDto = new TaskDto
        (
            Id: task.Id,
            Title: task.Title.Value,
            Description: task.Description.Value,
            Priority: task.Priority.ToString(),
            Status: task.Status.ToString(),
            DueDate: task.DueDate?.Value,
            Items: task.Items.Select(i => new TaskItemDto
            (
                Id: i.Id,
                Content: i.Content,
                IsCompleted: i.IsCompleted
            )).ToList(),
            Comments: task.Comments.Select(c => new CommentDto
            (
                Id: c.Id,
                Content: c.Content,
                Author: c.Author
            )).ToList()
        );

        return Result<CreateTaskResult>.Success(new CreateTaskResult(resultDto));
    }
}
