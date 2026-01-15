using Microsoft.AspNetCore.Mvc;
using Shared.Application.Results;

namespace Tasking.API;

[ApiController]
[Route("api/tasks")]
public abstract class BaseController : ControllerBase
{
    protected ActionResult HandleResult(Result result, int statusCode)
    {
        if (result.IsSuccess) return StatusCode(statusCode);

        return result.Error.Code switch
        {
            "Task.NotFound" => NotFound(result.Error),
            "Task.AlreadyExists" => Conflict(result.Error),
            "Auth.Unauthenticated" => Unauthorized(result.Error),
            "Auth.Forbidden" => Forbid(),
            _ => BadRequest(result.Error)
        };
    }

    protected ActionResult<T> HandleResult<T>(Result<T> result, int statusCode)
    {
        if (result.IsSuccess) return StatusCode(statusCode, result.Value);

        return result.Error.Code switch
        {
            "Task.NotFound" => NotFound(result.Error),
            "Task.AlreadyExists" => Conflict(result.Error),
            "Auth.Unauthenticated" => Unauthorized(result.Error),
            "Auth.Forbidden" => Forbid(),
            _ => BadRequest(result.Error)
        };
    }
}
