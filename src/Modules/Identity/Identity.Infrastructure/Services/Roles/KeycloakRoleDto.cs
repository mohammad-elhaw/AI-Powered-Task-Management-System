namespace Identity.Infrastructure.Services.Roles;

internal sealed record KeycloakRoleDto
{
    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
}
