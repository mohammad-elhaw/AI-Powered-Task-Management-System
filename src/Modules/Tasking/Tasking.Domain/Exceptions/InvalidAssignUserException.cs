namespace Tasking.Domain.Exceptions;

internal class InvalidAssignUserException(string message) : DomainException(message)
{
}