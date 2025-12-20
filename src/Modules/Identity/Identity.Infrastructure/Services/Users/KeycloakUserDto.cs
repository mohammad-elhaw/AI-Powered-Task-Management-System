namespace Identity.Infrastructure.Services.Users;

public record KeycloakUserDto
{
    public string Id { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public bool Enabled { get; init; }
}
