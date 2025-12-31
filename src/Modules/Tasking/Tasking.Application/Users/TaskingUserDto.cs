namespace Tasking.Application.Users;

public record TaskingUserDto(
    Guid UserId,
    string DisplayName,
    string Email,
    bool IsActive);