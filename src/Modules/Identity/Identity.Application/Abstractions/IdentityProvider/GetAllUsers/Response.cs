namespace Identity.Application.Abstractions.IdentityProvider.GetAllUsers;

public record Response(IReadOnlyList<UserDto> Users);

public record UserDto(Guid Id, string KeycloakId, string Email,
    string FirstName, string LastName, bool IsActive, List<string> RoleNames);