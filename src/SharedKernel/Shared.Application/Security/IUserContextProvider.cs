namespace Shared.Application.Security;

public interface IUserContextProvider
{
    Task<UserContext> Get();
}
