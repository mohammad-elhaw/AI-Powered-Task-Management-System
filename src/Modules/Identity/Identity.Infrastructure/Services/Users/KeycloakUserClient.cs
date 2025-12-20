using Identity.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace Identity.Infrastructure.Services.Users;

public sealed class KeycloakUserClient(
    HttpClient httpClient,
    IConfiguration configuration,
    IKeycloakTokenProvider keycloakTokenProvider)
{

    private const string RealmConfigKey = "KeyCloak:Realm";
    private const string BaseUrlConfigKey = "KeyCloak:BaseUrl";

    public async Task<bool> UserExists(string userName, CancellationToken cancellationToken)
    {
        await keycloakTokenProvider.GetAccessToken(cancellationToken);
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;

        var usersEndpoint = $"{baseUrl}/admin/realms/{realm}/users?username={Uri.EscapeDataString(userName)}";
        var response = await httpClient.GetAsync(usersEndpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        var users = JsonDocument
            .Parse(await response.Content.ReadAsStringAsync(cancellationToken)).RootElement;
        return users.GetArrayLength() > 0;
    }

    public async Task<IReadOnlyList<KeycloakUserDto>> GetAllUsers(CancellationToken cancellationToken)
    {
        await keycloakTokenProvider.GetAccessToken(cancellationToken);
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;
        var endpoint = $"{baseUrl}/admin/realms/{realm}/users";
        var response = await httpClient.GetAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer
            .Deserialize<List<KeycloakUserDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

    }

    public async Task<string> CreateUser(object payload, CancellationToken cancellationToken)
    {
        await keycloakTokenProvider.GetAccessToken(cancellationToken);
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;
        var usersEndpoint = $"{baseUrl}/admin/realms/{realm}/users";

        var response = await httpClient.PostAsJsonAsync(usersEndpoint, payload, cancellationToken);
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
        await keycloakTokenProvider.GetAccessToken(cancellationToken);
        var realm = configuration[RealmConfigKey]!;
        var baseUrl = configuration[BaseUrlConfigKey]!;
        var deleteEndpoint = $"{baseUrl}/admin/realms/{realm}/users/{keyCloakUserId}";
        var response = await httpClient.DeleteAsync(deleteEndpoint, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
