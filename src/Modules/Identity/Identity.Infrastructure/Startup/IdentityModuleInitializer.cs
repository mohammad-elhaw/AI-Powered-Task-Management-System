using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Seeding;

namespace Identity.Infrastructure.Startup;

public class IdentityModuleInitializer : IModuleInitializer
{
    public async Task MigrateAsync(IServiceProvider sp)
    {
        var db = sp.GetRequiredService<IdentityDbContext>();
        await db.Database.MigrateAsync();
    }

    public Task SeedAsync(IServiceProvider sp)
    {
        var seeders = sp.GetServices<IDataSeeder>();
        var tasks = seeders.Select(s => s.Seed(CancellationToken.None));
        return Task.WhenAll(tasks);
    }
}
