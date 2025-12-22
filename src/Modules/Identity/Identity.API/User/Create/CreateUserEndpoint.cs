using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.User.Create;

public class CreateUserEndpoint(IMediator mediator)
    : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserRequest request)
    {
        var result = await mediator.Send(request.ToCommand());

        var response = MapResult(result, r => new CreateUserResponse(r.UserDto));

        return HandleResult(response, StatusCodes.Status201Created);
    }
}
