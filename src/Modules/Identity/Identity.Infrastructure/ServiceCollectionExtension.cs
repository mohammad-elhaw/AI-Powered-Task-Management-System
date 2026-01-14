using Identity.Application.Abstractions;
using Identity.Application.Abstractions.IdentityProvider;
using Identity.Application.Abstractions.Permissions;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.KeycloakClient;
using Identity.Infrastructure.Services.Roles;
using Identity.Infrastructure.Services.Users;
using Identity.Infrastructure.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Security;
using Shared.Infrastructure.Seeding;
using Shared.Messaging;

namespace Identity.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<IdentityDbContext>(opts =>
        {
            opts.UseNpgsql(config.GetConnectionString("IdentityDatabase"));
        });

        services.AddScoped<DbContext>(sp => sp.GetRequiredService<IdentityDbContext>());

        services.AddCapPublisher<IdentityDbContext>(config);

        services.AddScoped<IDataSeeder, RoleSeeder>();
        services.AddScoped<IModuleInitializer, IdentityModuleInitializer>();

        services.AddScoped<IRoleRepository, EfRoleRepository>();
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserPermissionsRepository, UserPermissionsRepository>();
        services.AddScoped<IUserPermissionService, UserPermissionService>();

        services.AddScoped<IIdentityProvider, KeyCloakIdentityProvider>();
        services.AddScoped<IKeycloakTokenProvider, KeycloakTokenProvider>();
        services.AddScoped<KeycloakUserClient>();
        services.AddScoped<KeycloakRoleClient>();

        var keycloakAddress = config.GetSection("KeyCloak:BaseUrl")!;
        services.AddTransient<KeycloakAuthHandler>();
        services.AddHttpClient<IClient, Client>("keycloakUserClient", conf =>
        {
            conf.BaseAddress = new Uri(keycloakAddress.Value!);
        }).AddHttpMessageHandler<KeycloakAuthHandler>();

        services.AddScoped<IClient, Client>();

        return services;
    }
}
