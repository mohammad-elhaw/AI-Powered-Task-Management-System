using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasking.Application.Tasks.CreateTask;

namespace Tasking.API.CreateTask;

public class CreateTaskEndpoint(IMediator mediator)
    : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateTaskResult>> CreateTask([FromBody] CreateTaskRequest request)
    {
        var result = await mediator.Send(request.ToCommand());
        return HandleResult(result, StatusCodes.Status201Created);
    }
}
