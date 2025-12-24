namespace Identity.Domain.Exceptions.User;

public class InvalidUserKeycloakIdException : DomainException
{
    public InvalidUserKeycloakIdException()
        : base("Keycloak Id can not be empty.")
    {
    }
}
