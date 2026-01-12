namespace Identity.Domain.Exceptions.Permission;

public class InvalidPermissionCodeException : DomainException
{
    public InvalidPermissionCodeException()
        : base("Permission code is invalid. It cannot be null or empty.")
    {}
}
