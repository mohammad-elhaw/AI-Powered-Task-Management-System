using Identity.Infrastructure.Services.KeycloakClient;

namespace Identity.Infrastructure.Services.Users;

internal sealed class KeycloakUserClient(
    IClient keycloakClient)
{
    public async Task<bool> UserExists(UserExistsRequest request, CancellationToken cancellationToken)
    {
        var path = "/admin/realms/{0}/users";
        var response = await keycloakClient.SendAsync<List<KeycloakUserDto>>(
            new BaseRequest(path,queryParameters:request.ToQueryParameters()), cancellationToken);

        return response is { Count: > 0 };
    }

    public async Task<IReadOnlyList<KeycloakUserDto>> GetAllUsers(CancellationToken cancellationToken)
    {
        var path = "/admin/realms/{0}/users";
        var response = await keycloakClient.SendAsync<IReadOnlyList<KeycloakUserDto>>(
            new BaseRequest(path), cancellationToken);

        return response ?? [];
    }

    public async Task<string> CreateUser(object payload, CancellationToken cancellationToken)
    {
        var usersEndpoint = "/admin/realms/{0}/users";

        var response = await keycloakClient.SendAsync<HttpResponseMessage>(
            new BaseRequest(usersEndpoint, body: payload, method:HttpMethod.Post), cancellationToken);

        var locationHeader = response?.Headers.Location?.ToString();
        if (string.IsNullOrEmpty(locationHeader))
            throw new ArgumentException("Keycloak did not return Location header with user id");

        var parts = locationHeader.Split('/');
        var id = parts[^1];
        return id;
    }

    public async Task DeactivateUser(string keyCloakUserId, CancellationToken cancellationToken)
    {
        var deactivateEndpoint = "/admin/realms/{{0}}/users/{0}";
        var path = string.Format(deactivateEndpoint, keyCloakUserId);
        var payload = new
        {
            enabled = false
        };
        await keycloakClient.SendAsync<HttpResponseMessage>(
            new BaseRequest(path, body: payload, method: HttpMethod.Put), cancellationToken);
    }

    public async Task DeleteUser(string keyCloakUserId, CancellationToken cancellationToken)
    {
        var deleteEndpoint = "/admin/realms/{{0}}/users/{0}";

        var path = string.Format(deleteEndpoint, keyCloakUserId);
        await keycloakClient.SendAsync<HttpResponseMessage>(
            new BaseRequest(path, method: HttpMethod.Delete), cancellationToken);
    }
}
