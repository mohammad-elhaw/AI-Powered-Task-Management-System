using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Shared.Infrastructure.Security;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services,
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
