using Shared.Application.Results;
using Shared.Application.Security;
using Tasking.Application.Tasks.Security;

namespace Tasking.Application.Tasks.Security.Authorization.CreateTask;

public class CreateTaskPolicy : ICreateTaskPolicy
{
    public Result Check(UserContext user)
    {
        if (!user.HasPermission(TaskPermissions.Create))
            return Result.Failure(SecurityErrors.Forbidden(TaskPermissions.Create));

        return Result.Success();
    }
}
