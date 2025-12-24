namespace Identity.Domain.Exceptions.Role;

public class InvalidRoleNameException : DomainException
{
    public InvalidRoleNameException()
        : base("The role name is invalid. it can't be null or empty")
    {
    }
}
