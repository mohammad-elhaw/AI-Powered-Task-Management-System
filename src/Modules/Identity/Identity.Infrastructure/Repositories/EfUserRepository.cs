using Identity.Domain.Aggregates;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class EfUserRepository(IdentityDbContext context)
    : IUserRepository
{
    public Task Add(User user, CancellationToken cancellationToken)
    {
        context.Users.Add(user);
        return Task.CompletedTask;
    }

    public Task Delete(User user, CancellationToken cancellationToken)
    {
        context.Users.Remove(user);
        return Task.CompletedTask;
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken)
        => await context.Users
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);
}
