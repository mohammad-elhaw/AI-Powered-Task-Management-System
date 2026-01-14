namespace Shared.Infrastructure.Seeding;

public interface IDataSeeder
{
    Task Seed(CancellationToken cancellationToken);
}
