using Identity.Domain.Aggregates;

namespace Identity.Domain.Repositories;

public interface IRoleRepository
{
    Task Add(Role role, CancellationToken cancellationToken);
    Task<Role?> GetByName(string name, CancellationToken cancellationToken);
    Task<int> SaveChanges(CancellationToken cancellationToken);
}
