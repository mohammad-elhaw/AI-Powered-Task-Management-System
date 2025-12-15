using Identity.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace Identity.Infrastructure.Services;

public class KeyCloakIdentityProvider(
    HttpClient httpClient,
    IConfiguration configuration)
    : IIdentityProvider
{
    private const string RealmConfigKey = "KeyCloak:Realm";
    private const string BaseUrlConfigKey = "KeyCloak:BaseUrl";

    private string token;
    private DateTime tokenExpireAt = DateTime.MinValue;

    private async Task AuthenticateUser(CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(token) && tokenExpireAt > DateTime.UtcNow.AddMinutes(1)) return;
        
        var clientId = configuration["KeyCloak:ClientId"]!;
        var clientSecret = configuration["KeyCloak:ClientSecret"]!;
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;

        var tokenEndpoint = $"{baseUrl}/realms/{realm}/protocol/openid-connect/token";

        var content = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        ]);

        var response = await httpClient.PostAsync(tokenEndpoint, content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync(cancellationToken));
        token = doc.RootElement.GetProperty("access_token").GetString()!;
        var expireIn = doc.RootElement.GetProperty("expires_in").GetInt32();
        tokenExpireAt = DateTime.UtcNow.AddSeconds(expireIn - 60); // Refresh 1 minute before expiry

        httpClient.DefaultRequestHeaders.Remove("Authorization");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    private async Task<bool> UserExists(string userName, CancellationToken cancellationToken)
    {
        await AuthenticateUser(cancellationToken);
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;

        var usersEndpoint = $"{baseUrl}/admin/realms/{realm}/users?username={Uri.EscapeDataString(userName)}";
        var response = await httpClient.GetAsync(usersEndpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        var users = JsonDocument
            .Parse(await response.Content.ReadAsStringAsync(cancellationToken)).RootElement;
        return users.GetArrayLength() > 0;
    }

    public async Task<string> CreateUser(string userName, string email, string firstName, 
        string lastName, string tempPassword, CancellationToken cancellationToken)
    {
        if (await UserExists(userName, cancellationToken))
            throw new InvalidOperationException($"User '{userName}' already exists.");

        await AuthenticateUser(cancellationToken);
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;
        var usersEndpoint = $"{baseUrl}/admin/realms/{realm}/users";
        var userPayload = new
        {
            username = userName,
            email = email,
            firstName = firstName,
            lastName = lastName,
            enabled = true,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    value = tempPassword,
                    temporary = true
                }
            }
        };

        var response = await httpClient.PostAsJsonAsync(usersEndpoint, userPayload, cancellationToken);
        response.EnsureSuccessStatusCode();

        var locationHeader = response.Headers.Location?.ToString();
        if (string.IsNullOrEmpty(locationHeader))
            throw new ArgumentException("Keycloak did not return Location header with user id");

        var parts = locationHeader.Split('/');
        var id = parts[^1];
        return id;
    }
    public async Task DeleteUser(string keyCloakUserId, CancellationToken cancellationToken)
    {
        await AuthenticateUser(cancellationToken);
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;
        var deleteEndpoint = $"{baseUrl}/admin/realms/{realm}/users/{keyCloakUserId}";
        var response = await httpClient.DeleteAsync(deleteEndpoint, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<object>> GetAllUsers(CancellationToken cancellationToken)
    {
        await AuthenticateUser(cancellationToken);
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;
        var endpoint = $"{baseUrl}admin/realms/{realm}/users";
        var response = await httpClient.GetAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<object>>(json)!;
    }

    public async Task EnsureRealmRoleExists(string roleName, CancellationToken cancellationToken)
    {
        await AuthenticateUser(cancellationToken);
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

    public async Task AssignRole(string keyCloakUserId, string roleName, CancellationToken cancellationToken)
    {
        await AuthenticateUser(cancellationToken);
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
}
