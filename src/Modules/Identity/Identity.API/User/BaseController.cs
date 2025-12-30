using Microsoft.AspNetCore.Mvc;
using Shared.Application.Results;

namespace Identity.API.User;

[Route("api/users")]
public abstract class BaseController :API.BaseController
{
    protected ActionResult HandleResult(Result result, int statusCode)
    {
        if (result.IsSuccess) return StatusCode(statusCode);

        return result.Error.Code switch
        {
            "User.NotFound" => NotFound(result.Error),
            "User.AlreadyExists" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    protected ActionResult<T> HandleResult<T>(Result<T> result, int statusCode)
    {
        if (result.IsSuccess) return StatusCode(statusCode, result.Value);

        return result.Error.Code switch
        {
            "User.NotFound" => NotFound(result.Error),
            "User.AlreadyExists" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}
