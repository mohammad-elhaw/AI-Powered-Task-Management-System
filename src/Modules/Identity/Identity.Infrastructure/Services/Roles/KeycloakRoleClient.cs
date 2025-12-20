using Identity.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace Identity.Infrastructure.Services.Roles;

public sealed class KeycloakRoleClient(
    HttpClient httpClient,
    IConfiguration configuration,
    IKeycloakTokenProvider keycloakTokenProvider)
{

    private const string RealmConfigKey = "KeyCloak:Realm";
    private const string BaseUrlConfigKey = "KeyCloak:BaseUrl";

    public async Task AssignRole(string keyCloakUserId, string roleName, CancellationToken cancellationToken)
    {
        await keycloakTokenProvider.GetAccessToken(cancellationToken);
        var realm = configuration[RealmConfigKey];
        var baseUrl = configuration[BaseUrlConfigKey];

        var roleResponse = await httpClient
            .GetAsync($"{baseUrl}/admin/realms/{realm}/roles/{roleName}", cancellationToken);
        roleResponse.EnsureSuccessStatusCode();

        var role = JsonDocument
            .Parse(await roleResponse.Content.ReadAsStringAsync(cancellationToken))
            .RootElement;

        var roleRepresentation = new[]
        {
            new
            {
                id = role.GetProperty("id").GetString(),
                name = role.GetProperty("name").GetString()
            }
        };

        var assignEndpoint = $"{baseUrl}/admin/realms/{realm}/users/{keyCloakUserId}/role-mappings/realm";
        var assignResponse = await httpClient
            .PostAsJsonAsync(assignEndpoint, roleRepresentation, cancellationToken);

        assignResponse.EnsureSuccessStatusCode();
    }


    public async Task EnsureRealmRoleExists(string roleName, CancellationToken cancellationToken)
    {
        await keycloakTokenProvider.GetAccessToken(cancellationToken);
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;

        var rolesEndpoint = $"{baseUrl}/admin/realms/{realm}/roles/{roleName}";

        var r = await httpClient.GetAsync(rolesEndpoint, cancellationToken);
        if (r.IsSuccessStatusCode) return; // exists

        var createEndpoint = $"{baseUrl}/admin/realms/{realm}/roles";
        var payload = new { name = roleName };
        var createResp = await httpClient.PostAsJsonAsync(createEndpoint, payload, cancellationToken);
        createResp.EnsureSuccessStatusCode();
    }

    public async Task<List<string>> GetUserRoles(
        string keycloakUserId,
        CancellationToken cancellationToken)
    {
        await keycloakTokenProvider.GetAccessToken(cancellationToken);

        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!.TrimEnd('/');

        var endpoint =
            $"{baseUrl}/admin/realms/{realm}/users/{keycloakUserId}/role-mappings/realm";

        var response = await httpClient.GetAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        var roles = await response.Content
            .ReadFromJsonAsync<List<KeycloakRoleDto>>(cancellationToken: cancellationToken);

        return roles?.Select(r => r.Name).ToList() ?? [];
    }

}
