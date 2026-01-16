namespace Tasking.Domain.Exceptions;

internal sealed class InvalidAuthorException(string message) 
    : DomainException(message)
{
}
