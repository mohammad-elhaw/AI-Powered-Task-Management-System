namespace Identity.Domain.Exceptions.Permission;

public class InvalidPermissionNameException : DomainException
{
    public InvalidPermissionNameException()
        : base("Permission name is invalid. It cannot be null or empty.")
    {
    }
}
