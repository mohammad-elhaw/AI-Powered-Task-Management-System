namespace Tasking.Domain.Exceptions;

internal sealed class InvalidContentException(string message) 
    : DomainException(message)
{
}
