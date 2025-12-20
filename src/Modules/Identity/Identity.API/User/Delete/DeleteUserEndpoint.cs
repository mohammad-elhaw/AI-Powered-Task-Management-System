using Identity.Application.Commands.DeleteUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.User.Delete;

public class DeleteUserEndpoint(IMediator mediator)
    : BaseController
{
    [HttpDelete("{UserId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteUser(Guid UserId)
    {
        await mediator.Send(new DeleteUserCommand(UserId));
        return NoContent();
    }
}
