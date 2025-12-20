using Identity.Application.Abstractions;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Roles;
using Identity.Infrastructure.Services.Users;
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
        services.AddScoped<IUserReadRepository, UserReadRepository>();

        services.AddScoped<IIdentityProvider, KeyCloakIdentityProvider>();
        services.AddScoped<IKeycloakTokenProvider, KeycloakTokenProvider>();

        services.AddTransient<KeycloakAuthHandler>();
        services.AddHttpClient<KeycloakUserClient>()
            .AddHttpMessageHandler<KeycloakAuthHandler>();
        services.AddHttpClient<KeycloakRoleClient>()
            .AddHttpMessageHandler<KeycloakAuthHandler>();
        return services;
    }
}
