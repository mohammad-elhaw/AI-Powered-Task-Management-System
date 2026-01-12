using Microsoft.AspNetCore.Http;
using Shared.Application.Security;
using System.Security.Claims;

namespace Shared.Infrastructure.Security;

public class UserContextProvider
    (IHttpContextAccessor http,
    IUserPermissionService permissionService)
    : IUserContextProvider
{
    public async Task<UserContext> Get()
    {

        if(http.HttpContext?.User?.Identity is null || !http.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("User is not authenticated");

        var keycloakId = http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            ?? throw new UnauthorizedAccessException("User is not authenticated");
        var permissions = await permissionService.GetUserPermissions(keycloakId);

        return new UserContext(keycloakId, permissions);
    }
}
