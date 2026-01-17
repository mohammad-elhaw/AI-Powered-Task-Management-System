using Identity.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Notifications.Infrastructure;
using Shared.Infrastructure;
using Tasking.API;

namespace TaskManagementSystem.Gateway.Extensions;

public static class InitializeApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(Tasking.API.ServiceCollectionExtension).Assembly)
            .AddApplicationPart(typeof(Identity.API.ServiceCollectionExtension).Assembly)
            .ConfigureApiBehaviorOptions(opts =>
            {
                opts.SuppressModelStateInvalidFilter = true;
            });

        services.AddSwaggerServices();

        services.AddTaskingAPI(configuration);
        services.AddIdentityAPI(configuration);
        services.AddNotificationsInfrastructure(configuration);

        services.AddSharedInfrastructure(
            typeof(Tasking.Application.ServiceCollectionExtension).Assembly,
            typeof(Identity.Application.ServiceCollectionExtension).Assembly);

        services.AddKeycloakAuthentication(configuration);

        return services;
    }

    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", new()
            {
                Title = "Task Management System API",
                Version = "v1",
                Description = "API for Task Management System."
            });

            opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

            opts.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

        });
        return services;
    }


    private static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {

        var keycloak = configuration.GetSection("Keycloak");
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority =
                    $"{keycloak["BaseUrl"]}/realms/{keycloak["Realm"]}";

                options.Audience = keycloak["ClientId"];

                options.RequireHttpsMetadata = false; // DEV ONLY

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    // Keycloak-specific
                    NameClaimType = "preferred_username",
                    RoleClaimType = "roles"
                };
            });

        services.AddAuthorization();
        return services;
    }
}
