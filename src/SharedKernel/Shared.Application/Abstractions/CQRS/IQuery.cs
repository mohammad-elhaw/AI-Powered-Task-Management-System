using MediatR;

namespace Shared.Application.Abstractions.CQRS;

public interface IQuery<out T> : IRequest<T>
    where T : notnull
{
}
