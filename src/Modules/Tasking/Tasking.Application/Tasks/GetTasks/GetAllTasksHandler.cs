using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;
using Tasking.Domain.Repositories;

namespace Tasking.Application.Tasks.GetTasks;

internal class GetAllTasksHandler(
    ITaskRepository taskRepository)
    : IQueryHandler<GetAllTasksQuery, GetAllTasksResult>
{
    public async Task<Result<GetAllTasksResult>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.GetAllTasks(cancellationToken);

        var results = tasks
            .Select(task => new TaskDto
            (
                Id: task.Id,
                Title: task.Title.Value,
                Description: task.Description.Value,
                Priority: task.Priority.ToString(),
                Status: task.Status.ToString(),
                DueDate: task.DueDate!.Value,
                Items: task.Items.Select(item => new TaskItemDto
                (
                    Id: item.Id,
                    Content: item.Content,
                    IsCompleted: item.IsCompleted
                )).ToList(),
                Comments: task.Comments.Select(comment => new CommentDto
                (
                    Id: comment.Id,
                    Content: comment.Content,
                    Author: comment.Author
                )).ToList()
            )).ToList();

        return Result<GetAllTasksResult>.Success(new GetAllTasksResult(results));

    }
}
