using Identity.Application.Abstractions;
using System.Net.Http.Headers;

namespace Identity.Infrastructure.Services;

public sealed class KeycloakAuthHandler(
    IKeycloakTokenProvider keycloakTokenProvider)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await keycloakTokenProvider.GetAccessToken(cancellationToken);

        request.Headers.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
