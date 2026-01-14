using Shared.Application.Results;
using Shared.Application.Security;

namespace Tasking.Application.Tasks.Security.Authorization.AssignTask;

public interface IAssignTaskPolicy
{
    Result Check(UserContext user, Domain.Aggregates.Task task);
}
