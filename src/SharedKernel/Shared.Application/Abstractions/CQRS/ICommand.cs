using MediatR;
using Shared.Application.Results;

namespace Shared.Application.Abstractions.CQRS;

public interface ICommand : IRequest<Result<Unit>>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
