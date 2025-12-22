using MediatR;
using Shared.Application.Results;

namespace Shared.Application.Abstractions.CQRS;

public interface ICommandHandler<in TCommand> :  IRequestHandler<TCommand, Result<Unit>> 
    where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse> 
    : IRequestHandler<TCommand, Result<TResponse>> 
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}
