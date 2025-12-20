namespace Identity.Application.Abstractions;

public interface IKeycloakTokenProvider
{
    Task<string> GetAccessToken(CancellationToken cancellationToken);
}
