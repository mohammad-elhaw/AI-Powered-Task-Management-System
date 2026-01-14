using Shared.Application.Results;

namespace Tasking.Application.Tasks.Errors;

public static class TaskErrors
{
    public static readonly Error TaskNotFound =
        new("Task.NotFound", "Task not found.", default);
    
    public static readonly Error TaskCompleted =
        new("Task.AlreadyCompleted", "Task is already completed.", default);
}
