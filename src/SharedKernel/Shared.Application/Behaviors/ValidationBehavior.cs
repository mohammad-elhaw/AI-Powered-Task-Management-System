using FluentValidation;
using MediatR;
using Shared.Application.Results;
using System.Reflection;

namespace Shared.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = 
        validators ?? [];

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            var errors = failures
                .Select(f => new { f.PropertyName, f.ErrorMessage })
                .Distinct()
                .ToList();

            // If the pipeline's response is Result<TR>, return Result<TR>.Failure(Error)
            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var innerType = typeof(TResponse).GetGenericArguments()[0];
                var error = new Error(
                    "Validation.Failed",
                    "One or More validation Errors Occured",
                    errors);

                var resultType = typeof(Result<>).MakeGenericType(innerType);
                var failureMethod = resultType.GetMethod("Failure", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(Error) }, null);
                if (failureMethod is not null)
                {
                    var failureResult = failureMethod.Invoke(null, [error])!;
                    return (TResponse)failureResult;
                }
            }

            // Fallback: throw validation exception if response type isn't Result<TR>
            throw new ValidationException(failures);
        }

        return await next(cancellationToken);
    }
}
