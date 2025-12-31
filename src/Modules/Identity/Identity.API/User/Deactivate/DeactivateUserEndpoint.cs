using Identity.Application.Commands.DeactivateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.User.Deactivate;

public class DeactivateUserEndpoint(IMediator mediator)
    : BaseController
{
    [HttpPost("{UserId:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Unit>> DeactivateUser(Guid UserId)
    {
        var result = await mediator.Send(new DeactivateUserCommand(UserId));
        return HandleResult(result, StatusCodes.Status204NoContent);
    }
}
