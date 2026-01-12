using Shared.Application.Results;

namespace Tasking.Application.Errors;

public static class TaskErrors
{
    public static readonly Error TaskNotFound =
        new("Task.NotFound", "Task not found.", default);
    public static readonly Error NotAuthorizedToAssignTask =
        new("Task.Assign.NotAuthorized", "User is not authorized to assign this task.", default);
}
