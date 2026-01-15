using Tasking.Application.Tasks.CreateTask;

namespace Tasking.API.CreateTask;

public static class CreateTaskRequestMapping
{
    public static CreateTaskCommand ToCommand(this CreateTaskRequest request)
        => new(
            request.Title,
            request.Description,
            request.Priority,
            request.Status,
            request.DueDate,
            (request.Items ?? [])
                .Select(i => new CreateTaskItemModel(i.Content ?? string.Empty, i.IsCompleted))
                .ToList(),
            (request.Comments ?? [])
                .Select(c => new CreateCommentModel(c.Content, c.Author ?? string.Empty))
                .ToList()
        );
}
