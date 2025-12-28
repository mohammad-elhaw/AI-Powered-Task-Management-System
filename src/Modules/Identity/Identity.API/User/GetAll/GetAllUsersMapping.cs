using Identity.Application.Queries.User.GetUsers;

namespace Identity.API.User.GetAll;

public static class GetAllUsersMapping
{
    public static GetAllUsersResponse ToGetAllUsersResponse(this GetUsersResult result)
        => new GetAllUsersResponse
        (
            result.Users.Select(u => new GetAllUsersDto(
                Id: u.Id,
                Email: u.Email,
                FirstName: u.FirstName,
                LastName: u.LastName,
                IsActive: u.IsActive,
                Roles: u.RoleNames)).ToList()
        );
}
