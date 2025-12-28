using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Identity.Infrastructure.Services.KeycloakClient;

internal class Client(IConfiguration configuration,IHttpClientFactory httpClientFactory) : IClient
{
    private const string RealmConfigKey = "KeyCloak:Realm";

    public  async Task<TResponse?> SendAsync<TResponse>(BaseRequest request, CancellationToken cancellationToken)
    {
        var realm = configuration[RealmConfigKey]!;

        var queryString = QueryHelpers.AddQueryString("", request.QueryParameters);

        var path = string.Format(request.Path, realm);

        var relativeUri = path + queryString;

        var jsonBody = JsonSerializer.Serialize(request.Body);
        var httpRequestMessage = new HttpRequestMessage(request.Method, relativeUri);

        if(request.Body is not null)
        {
            httpRequestMessage.Content =
                new StringContent(
                    jsonBody,                 
                    Encoding.UTF8,
                    "application/json");
        }

        var httpClient = httpClientFactory.CreateClient("keycloakUserClient");

       var response =  await httpClient.SendAsync(httpRequestMessage, cancellationToken);
       response.EnsureSuccessStatusCode();

        var stringResponse = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(stringResponse))
        {
            if (typeof(TResponse) == typeof(HttpResponseMessage))
                return (TResponse)(object)response;

            return default;
        }

        var deserialized = JsonSerializer.Deserialize<TResponse>(stringResponse,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return deserialized;
        
    }
}
