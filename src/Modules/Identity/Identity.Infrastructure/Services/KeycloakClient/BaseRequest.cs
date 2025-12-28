namespace Identity.Infrastructure.Services.KeycloakClient;

internal record BaseRequest
{
    public BaseRequest(string path, object? body=null, Dictionary<string, string?>? queryParameters=null, HttpMethod? method=null)
    {
        Method = method??HttpMethod.Get;
        Path = path;
        Body = body;
        QueryParameters = queryParameters ??[];
    }

    public HttpMethod Method { get; init; } = HttpMethod.Get;
    public string Path { get; init; }
    public object? Body { get; init; }
    public Dictionary<string, string?> QueryParameters { get; init; } = [];
}
