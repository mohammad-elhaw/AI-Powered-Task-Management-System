namespace Identity.Application.Dtos;

public record UserDto(Guid Id, string KeycloakId, string Email, 
    string FirstName, string LastName, bool IsActive, List<string> RoleNames);