using MediatR;

namespace Shared.Application.Abstractions.CQRS;

public interface ICommand : IRequest<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
