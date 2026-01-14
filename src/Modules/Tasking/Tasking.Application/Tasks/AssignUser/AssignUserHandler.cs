using MediatR;
using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;
using Shared.Application.Security;
using Tasking.Application.Tasks.Errors;
using Tasking.Application.Tasks.Security.Authorization.AssignTask;
using Tasking.Domain.Repositories;

namespace Tasking.Application.Tasks.AssignUser;

internal class AssignUserHandler(
    ITaskRepository taskRepository,
    IUserContextProvider userContext,
    IAssignTaskPolicy assignPolicy)
    : ICommandHandler<AssignUserCommand>
{
    public async Task<Result<Unit>> Handle(AssignUserCommand command, CancellationToken cancellationToken)
    {

        var task = await taskRepository.GetTask(command.TaskId, cancellationToken);
        var user = await userContext.GetAsync();

        if(user.IsFailure)
            return Result<Unit>.Failure(user.Error);

        if (task is null)
            return Result<Unit>.Failure(TaskErrors.TaskNotFound);

        var assignCheck = assignPolicy.Check(user.Value!, task);
        if (assignCheck.IsFailure)
            return Result<Unit>.Failure(assignCheck.Error);

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
