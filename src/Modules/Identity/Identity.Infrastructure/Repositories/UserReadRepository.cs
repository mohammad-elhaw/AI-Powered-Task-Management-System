using Identity.Application.Abstractions;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class UserReadRepository(IdentityDbContext context)
    : IUserReadRepository
{
    public async Task<IReadOnlyDictionary<string, List<string>>> GetRolesByKeycloakIds(List<string> keycloakIds, CancellationToken cancellationToken)
    {
        var usersWithRoles = await context.Users
            .Where(u => keycloakIds.Contains(u.KeycloakId))
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();

        var rolesDic = usersWithRoles.ToDictionary(
            u => u.KeycloakId,
            u => u.UserRoles.Select(ur => ur.Role.Name).ToList());
        return rolesDic;
    }

    public async Task<IReadOnlyDictionary<string, Guid>> GetUserIdsByKeycloakIds(IEnumerable<string> keycloakIds, CancellationToken cancellationToken)
    {
        return await context.Users
            .Where(u => keycloakIds.Contains(u.KeycloakId))
            .Select(u => new { u.KeycloakId, u.Id })
            .ToDictionaryAsync(x => x.KeycloakId,
            x => x.Id,
            cancellationToken);
    }
}
