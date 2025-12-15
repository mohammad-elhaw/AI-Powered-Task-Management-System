using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Commands.CreateUser;

public record CreateUserCommand(string UserName, string Email, string FirstName, string LastName, 
    List<string> RoleNames) : ICommand<CreateUserResult>;
