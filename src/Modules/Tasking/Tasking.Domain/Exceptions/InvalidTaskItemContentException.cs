namespace Tasking.Domain.Exceptions;

internal sealed class InvalidTaskItemContentException(string message)
    : DomainException(message)
{
}
