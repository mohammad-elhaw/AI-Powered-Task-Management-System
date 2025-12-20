using Identity.Application.Queries.User.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.User.GetAll;

public class GetAllUsersEndpoint(IMediator mediator)
    : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetAllUsersResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetAllUsersResponse>> GetAllUsers()
    {
        var result = await mediator.Send(new GetUsersQuery());
        return Ok(new GetAllUsersResponse
        (
            [.. result.Users]
        ));
    }
}
