using Microsoft.AspNetCore.Http;
using Shared.Application.Results;
using Shared.Application.Security;
using System.Security.Claims;

namespace Shared.Infrastructure.Security;

public class UserContextProvider
    (IHttpContextAccessor http,
    IUserPermissionService permissionService)
    : IUserContextProvider
{
    public async Task<Result<UserContext>> GetAsync()
    {

        if(http.HttpContext?.User?.Identity is null || !http.HttpContext.User.Identity.IsAuthenticated)
            return Result<UserContext>.Failure(SecurityErrors.Unauthenticated);

        var keycloakId = http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        
        if(keycloakId is null)
            return Result<UserContext>.Failure(SecurityErrors.Unauthenticated);

        var permissions = await permissionService.GetUserPermissions(keycloakId);

        return Result<UserContext>.Success(new UserContext(keycloakId, permissions));
    }
}
