using Identity.Infrastructure.Services.KeycloakClient;
using MediatR;

namespace Identity.Infrastructure.Services.Roles;

internal sealed class KeycloakRoleClient(
    IClient keycloakClient)
{

    public async Task AssignRole(string keyCloakUserId, string roleName, CancellationToken cancellationToken)
    {
        var endpoint = "/admin/realms/{{0}}/roles/{0}";
        var path = string.Format(endpoint, roleName);

        var roleResponse = await keycloakClient.SendAsync<KeycloakRoleDto>(
            new BaseRequest(path), cancellationToken)
            ?? throw new InvalidOperationException("Role response was null.");

        var assignEndpoint = "/admin/realms/{{0}}/users/{0}/role-mappings/realm";
        var assignPath = string.Format(assignEndpoint, keyCloakUserId);

        var payload = new[]
        {
            new
            {
                id = roleResponse.Id,
                name = roleResponse.Name
            }
        };

        await keycloakClient.SendAsync<Unit>(
            new BaseRequest(
                assignPath, 
                body: payload, method: HttpMethod.Post),
                cancellationToken);

    }


    public async Task EnsureRealmRoleExists(string roleName, CancellationToken cancellationToken)
    {

        var endpoint = "/admin/realms/{{0}}/roles/{0}";
        var path = string.Format(endpoint, roleName);

        var r = await keycloakClient.SendAsync<HttpResponseMessage>(
            new BaseRequest(path), cancellationToken);
        if (r!.IsSuccessStatusCode) return; // exists

        var createEndpoint = "/admin/realms/{0}/roles";
        var payload = new { name = roleName };
        await keycloakClient.SendAsync<HttpResponseMessage>(
            new BaseRequest(
                createEndpoint, 
                body: payload, 
                method: HttpMethod.Post)
            , cancellationToken);
    }

    public async Task<List<string>> GetUserRoles(
        string keycloakUserId,
        CancellationToken cancellationToken)
    {

        var endpoint =
            "/admin/realms/{{0}}/users/{0}/role-mappings/realm";

        var path = string.Format(endpoint, keycloakUserId);

        var response = await keycloakClient.SendAsync<List<KeycloakRoleDto>>(
            new BaseRequest(path), cancellationToken);

        return response?.Select(r => r.Name).ToList() ?? [];
    }

}
