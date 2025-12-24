namespace Identity.Domain.Exceptions.User;

public class InvalidUserFullNameException(string message) : DomainException(message)
{
}
