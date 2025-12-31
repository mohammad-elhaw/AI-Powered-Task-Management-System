using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Commands.DeactivateUser;

public record DeactivateUserCommand(Guid UserId) : ICommand;