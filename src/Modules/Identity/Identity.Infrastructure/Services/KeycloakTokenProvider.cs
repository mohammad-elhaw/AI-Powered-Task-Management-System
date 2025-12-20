using Identity.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Identity.Infrastructure.Services;

public sealed class KeycloakTokenProvider(
    HttpClient httpClient,
    IConfiguration configuration) 
    : IKeycloakTokenProvider
{

    private const string RealmConfigKey = "KeyCloak:Realm";
    private const string BaseUrlConfigKey = "KeyCloak:BaseUrl";

    private string? token;
    private DateTime tokenExpireAt = DateTime.MinValue;

    public async Task<string> GetAccessToken(CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(token) && tokenExpireAt > DateTime.UtcNow.AddMinutes(1)) 
            return token;

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

        return token;
    }
}