namespace Identity.Infrastructure.Services.KeycloakClient;

internal interface IClient
{
    Task<TResponse?> SendAsync<TResponse>(BaseRequest request,CancellationToken cancellationToken);
}
