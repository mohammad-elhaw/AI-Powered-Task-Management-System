using Identity.Application.Dtos;

namespace Identity.API.User.GetAll;

public record GetAllUsersResponse(List<UserDto> UserDtos);
