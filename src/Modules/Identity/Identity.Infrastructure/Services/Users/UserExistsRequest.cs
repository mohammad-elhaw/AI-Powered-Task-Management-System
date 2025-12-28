namespace Identity.Infrastructure.Services.Users;

public record UserExistsRequest(string UserName)
{
    public Dictionary<string, string?> ToQueryParameters() => new()
    {
        {"username", UserName },
    };
}
