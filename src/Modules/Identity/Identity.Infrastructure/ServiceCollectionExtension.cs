using Identity.Domain.Repositories;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services,
        IConfiguration config)
    {
        // Add infrastructure services here (e.g., database context, repositories, etc.)

        services.AddDbContext<IdentityDbContext>(opts =>
        {
            opts.UseNpgsql(config.GetConnectionString("IdentityDatabase"));
        });

        services.AddScoped<IRoleRepository, EfRoleRepository>();
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddHttpClient<IIdentityProvider, KeyCloakIdentityProvider>();
        return services;
    }
}
