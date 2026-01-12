using Identity.Application.Abstractions.Permissions;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class UserPermissionsRepository
    (IdentityDbContext context)
    : IUserPermissionsRepository
{
    public async Task<IReadOnlySet<string>> GetUserPermissions(string keyCloakId, 
        CancellationToken cancellationToken)
    {
        return await context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.KeycloakId == keyCloakId)
            .SelectMany(u => u.UserRoles)
            .SelectMany(u => u.Role.Permissions)
            .Select(p => p.Permission.Code)
            .ToHashSetAsync(cancellationToken);
    }
}
