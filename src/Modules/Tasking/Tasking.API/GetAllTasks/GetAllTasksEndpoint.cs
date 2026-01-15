using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasking.Application.Tasks.GetTasks;

namespace Tasking.API.GetAllTasks;

public class GetAllTasksEndpoint(IMediator mediator)
    : BaseController
{
    [HttpGet]
    public async Task<ActionResult<GetAllTasksResponse>> GetAllTasks()
    {
        var result = await mediator.Send(new GetAllTasksQuery());
        return HandleResult(result.ToGetAllTasksResponse(), StatusCodes.Status200OK);
    }
}
