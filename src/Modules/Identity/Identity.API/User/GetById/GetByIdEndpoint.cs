using Identity.Application.Queries.User.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.User.GetById;

public class GetByIdEndpoint(IMediator mediator)
    : BaseController
{
    [HttpGet("{UserId:guid}")]
    [ProducesResponseType(typeof(GetByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetByIdResponse>> GetUserById(Guid UserId)
    {
        var result = await mediator.Send(new GetByIdQuery(UserId));

        return Ok(new GetByIdResponse(result.UserDto));
    }
}
