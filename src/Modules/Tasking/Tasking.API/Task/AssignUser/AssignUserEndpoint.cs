using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasking.Application.Tasks.AssignUser;

namespace Tasking.API.Task.AssignUser;

public class AssignUserEndpoint(IMediator mediator)
    : BaseController
{
    [HttpPost("{TaskId:guid}/assign/{UserId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Unit>> AssignUser(Guid TaskId, Guid UserId)
    {
        var command = new AssignUserCommand(TaskId, UserId);
        var result = await mediator.Send(command);
        return HandleResult(result, StatusCodes.Status204NoContent);
    }
}
