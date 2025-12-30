using Microsoft.AspNetCore.Mvc;
using Shared.Application.Results;

namespace Identity.API;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected static Result<TResponse> MapResult<TSource, TResponse>(
        Result<TSource> result, Func<TSource, TResponse> map)
    {
        return result.IsSuccess
            ? Result<TResponse>.Success(map(result.Value!))
            : Result<TResponse>.Failure(result.Error);
    }
}
