using MediatR;
using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;
using Shared.Application.Security;
using Tasking.Application.Authorization;
using Tasking.Application.Errors;
using Tasking.Domain.Repositories;

namespace Tasking.Application.Commands.AssignUser;

internal class AssignUserHandler(
    ITaskRepository taskRepository,
    IUserContextProvider userContext,
    ITaskAuthorizationPolicy policy)
    : ICommandHandler<AssignUserCommand>
{
    public async Task<Result<Unit>> Handle(AssignUserCommand command, CancellationToken cancellationToken)
    {

        var task = await taskRepository.GetTask(command.TaskId, cancellationToken);
        var user = await userContext.Get();

        if(!await policy.CanAssign(user, task))
            return Result<Unit>.Failure(TaskErrors.NotAuthorizedToAssignTask);

        if (task is null)
            return Result<Unit>.Failure(TaskErrors.TaskNotFound);

        try
        {
            task.AssignUser(command.UserId);
            await taskRepository.SaveChanges(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
        catch(Exception ex)
        {
            return Result<Unit>.Failure(new Error("AssignUser.Failed", ex.Message, default));
        }
    }
}
