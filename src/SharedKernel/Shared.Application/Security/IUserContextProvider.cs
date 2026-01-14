using Shared.Application.Results;

namespace Shared.Application.Security;

public interface IUserContextProvider
{
    Task<Result<UserContext>> GetAsync();
}
