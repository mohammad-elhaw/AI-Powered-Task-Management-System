using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Commands.ActivateUser;

public record ActivateUserCommand(Guid UserId) : ICommand;
