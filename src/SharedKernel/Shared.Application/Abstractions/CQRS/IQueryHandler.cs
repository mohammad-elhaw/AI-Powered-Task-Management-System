using MediatR;

namespace Shared.Application.Abstractions.CQRS;

public interface IQueryHandler<in IQuery, TResponse>
    : IRequestHandler<IQuery, TResponse>
    where IQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
