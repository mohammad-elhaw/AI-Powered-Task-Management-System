namespace Identity.Application.Abstractions.IdentityProvider.CreateUser;

public record Request(string UserName, string Email,
    string FirstName, string LastName, string TempPassword);