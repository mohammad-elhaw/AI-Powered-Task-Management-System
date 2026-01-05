using Shared.Application.Abstractions.CQRS;

namespace Tasking.Application.Commands.AssignUser;

public record AssignUserCommand(Guid TaskId, Guid UserId): ICommand;