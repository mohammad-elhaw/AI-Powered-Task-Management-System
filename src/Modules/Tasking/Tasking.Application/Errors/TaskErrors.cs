using Shared.Application.Results;

namespace Tasking.Application.Errors;

public static class TaskErrors
{
    public static readonly Error TaskNotFound =
        new("Task.NotFound", "Task not found.", default);
}
