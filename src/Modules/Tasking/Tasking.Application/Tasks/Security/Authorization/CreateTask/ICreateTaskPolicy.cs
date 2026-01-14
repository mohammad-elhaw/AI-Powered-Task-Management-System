using Shared.Application.Results;
using Shared.Application.Security;

namespace Tasking.Application.Tasks.Security.Authorization.CreateTask;

public interface ICreateTaskPolicy
{
    Result Check(UserContext user);
}
