using Shared.Application.Security;

namespace Tasking.Application.Authorization;

public class TaskAuthorizationPolicy : ITaskAuthorizationPolicy
{
    public Task<bool> CanAssign(UserContext user, Domain.Aggregates.Task task)
    {
        if(!user.HasPermission("Task.Assign"))
            return Task.FromResult(false);
        
        if(task.Status == Domain.Enums.TaskStatus.Completed)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }
}
