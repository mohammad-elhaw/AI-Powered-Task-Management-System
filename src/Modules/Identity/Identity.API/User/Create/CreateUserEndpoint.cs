using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.User.Create;

public class CreateUserEndpoint(IMediator mediator)
    : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var result = await mediator.Send(request.ToCommand());
        return StatusCode(StatusCodes.Status201Created, new CreateUserResponse(result.UserDto));
    }
}
