using Identity.Application.Queries.User.GetUsers;
using Shared.Application.Results;

namespace Identity.API.User.GetAll;

public static class GetAllUsersMapping
{
    public static Result<GetAllUsersResponse> ToGetAllUsersResponse(this Result<GetUsersResult> result)
    {
        if (result.IsFailure)
        {
            return Result<GetAllUsersResponse>.Failure(result.Error);
        }

        return Result<GetAllUsersResponse>.Success(new GetAllUsersResponse
        (
            result.Value!.Users.Select(u => new GetAllUsersDto(
                Id: u.Id,
                Email: u.Email,
                FirstName: u.FirstName,
                LastName: u.LastName,
                IsActive: u.IsActive,
                Roles: u.RoleNames)).ToList()
        ));
    }
}
