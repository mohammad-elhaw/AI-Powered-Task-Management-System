namespace Shared.Infrastructure.Seeding;

public interface IModuleInitializer
{
    Task MigrateAsync(IServiceProvider sp);
    Task SeedAsync(IServiceProvider sp);
}
