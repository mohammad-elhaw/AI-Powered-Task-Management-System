using MediatR;
using Shared.Application.Results;

namespace Shared.Application.Abstractions.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    where TResponse : notnull
{
}
