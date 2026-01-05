using MediatR;
using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;
using Tasking.Application.Errors;
using Tasking.Domain.Repositories;

namespace Tasking.Application.Commands.AssignUser;

internal class AssignUserHandler(
    ITaskRepository taskRepository)
    : ICommandHandler<AssignUserCommand>
{
    public async Task<Result<Unit>> Handle(AssignUserCommand request, CancellationToken cancellationToken)
    {

        var task = await taskRepository.GetTask(request.TaskId, cancellationToken);
        if(task is null)
            return Result<Unit>.Failure(TaskErrors.TaskNotFound);

        try
        {
            task.AssignUser(request.UserId);
            await taskRepository.SaveChanges(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
        catch(Exception ex)
        {
            return Result<Unit>.Failure(new Error("AssignUser.Failed", ex.Message, default));
        }
    }
}
