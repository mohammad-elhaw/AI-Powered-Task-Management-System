namespace Identity.API.User.GetAll;

public record GetAllUsersResponse(List<GetAllUsersDto> Data);
public record GetAllUsersDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    bool IsActive,
    IReadOnlyList<string> Roles
);