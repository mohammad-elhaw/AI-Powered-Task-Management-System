using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : ICommand;