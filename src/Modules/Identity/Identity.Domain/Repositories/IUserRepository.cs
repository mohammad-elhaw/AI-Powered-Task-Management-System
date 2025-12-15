namespace Identity.Domain.Repositories;

public interface IUserRepository
{
    Task Add(Aggregates.User user, CancellationToken cancellationToken);
    Task<Aggregates.User?> GetById(Guid id, CancellationToken cancellationToken);
    Task<int> SaveChanges(CancellationToken cancellationToken);
}
