using Shared.Application.Abstractions.CQRS;

namespace Tasking.Application.Tasks.AssignUser;

public record AssignUserCommand(Guid TaskId, Guid UserId): ICommand;