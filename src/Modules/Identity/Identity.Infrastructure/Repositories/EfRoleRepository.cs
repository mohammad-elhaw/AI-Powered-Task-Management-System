using Identity.Domain.Aggregates;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class EfRoleRepository(IdentityDbContext context)
    : IRoleRepository
{
    public Task Add(Role role, CancellationToken cancellationToken)
    {
        context.Roles.Add(role);
        return Task.CompletedTask;
    }

    public async Task<Role?> GetByName(string name, CancellationToken cancellationToken)
        => await context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);
}
