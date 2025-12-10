using Shared.Application.Abstractions.CQRS;
using Tasking.Domain.Repositories;

namespace Tasking.Application.Commands.AddTaskItem;

internal class AddTaskItemHandler(ITaskRepository repository)
    : ICommandHandler<AddTaskItemCommand, AddTaskItemResult>
{
    public async Task<AddTaskItemResult> Handle(AddTaskItemCommand request, CancellationToken cancellationToken)
    {
        var task = await repository.GetTask(request.TaskId, cancellationToken);

        if(task is null) { /*we should return result object */}
        task.AddItem(request.Content);
        await repository.UpdateTask(task, cancellationToken);

        var items = task.Items;
        var addedItem = items[items.Count - 1];

        return new AddTaskItemResult
        (
            TaskItemDto: new Dtos.TaskItemDto
            (
                Id: addedItem.Id,
                Content: addedItem.Content,
                IsCompleted: addedItem.IsCompleted
            )
        );

    }
}
