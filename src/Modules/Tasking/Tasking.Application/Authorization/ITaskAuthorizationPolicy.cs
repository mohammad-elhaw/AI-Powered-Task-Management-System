using Shared.Application.Security;

namespace Tasking.Application.Authorization;

public interface ITaskAuthorizationPolicy
{
    Task<bool> CanAssign(UserContext user, Domain.Aggregates.Task task);
}
