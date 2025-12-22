using Shared.Application.Results;

namespace Identity.Application.Errors;

public static class UserErrors
{
    public static readonly Error UserNotFound =
        new("User.NotFound", $"User not found.", default);

    public static readonly Error UserAlreadyExists =
        new("User.AlreadyExists", $"User already exists.", default);
}
