using Identity.Application.Dtos;

namespace Identity.Application.Queries.User.GetUsers;

public record GetUsersResult(IReadOnlyList<UserDto> Users);
