namespace Identity.Domain.Exceptions.User;

public class InvalidUserEmailException(string message) : DomainException(message)
{
}
