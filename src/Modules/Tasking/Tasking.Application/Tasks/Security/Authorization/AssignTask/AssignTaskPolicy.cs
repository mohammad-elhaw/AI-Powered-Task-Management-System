using Shared.Application.Results;
using Shared.Application.Security;
using Tasking.Application.Tasks.Errors;
using Tasking.Application.Tasks.Security;

namespace Tasking.Application.Tasks.Security.Authorization.AssignTask;

public class AssignTaskPolicy : IAssignTaskPolicy
{
    public Result Check(UserContext user, Domain.Aggregates.Task task)
    {
        if (!user.HasPermission(TaskPermissions.Assign))
            return Result.Failure(SecurityErrors.Forbidden(TaskPermissions.Assign));
        
        if(task.Status == Domain.Enums.TaskStatus.Completed)
            return Result.Failure(TaskErrors.TaskCompleted);

        return Result.Success();
    }
}
