using Identity.Application.Commands.ActivateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.User.Activate;

public class ActivateUserEndpoint(IMediator mediator)
    : BaseController
{
    [HttpPost("{UserId:guid}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Unit>> ActivateUser(Guid UserId)
    {
        var result = await mediator.Send(new ActivateUserCommand(UserId));
        return HandleResult(result, StatusCodes.Status204NoContent);
    }
}