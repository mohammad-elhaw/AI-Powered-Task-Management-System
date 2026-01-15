using MediatR;
using Shared.Application.Results;

namespace Shared.Application.Abstractions.CQRS;

public interface IQueryHandler<in IQuery, TResponse>
    : IRequestHandler<IQuery, Result<TResponse>>
    where IQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
