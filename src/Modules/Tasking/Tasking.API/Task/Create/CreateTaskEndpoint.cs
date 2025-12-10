using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tasking.API.Task.Create;

public class CreateTaskEndpoint(IMediator mediator)
    : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var result = await mediator.Send(request.ToCommand());
        return StatusCode(StatusCodes.Status201Created, result);
    }
}
