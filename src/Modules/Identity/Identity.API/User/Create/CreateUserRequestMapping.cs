using Identity.Application.Commands.CreateUser;

namespace Identity.API.User.Create;

public static class CreateUserRequestMapping
{
    public static CreateUserCommand ToCommand(this CreateUserRequest request)
        => new(
            request.UserName,
            request.Email,
            request.FirstName,
            request.LastName,
            request.RoleNames ?? new List<string>()
        );
}
