namespace Tasking.Domain.Exceptions;

internal abstract class DomainException(string message) : Exception(message);