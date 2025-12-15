namespace Identity.API.User.Create;

public record CreateUserRequest(string UserName, string Email, string FirstName, string LastName,
    List<string> RoleNames);
