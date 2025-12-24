using Identity.Domain.Exceptions.User;

namespace Identity.Domain.ValueObjects;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }

    private FullName() { }
    private FullName(string first, string last) 
    {
        FirstName = first;
        LastName = last;
    }

    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidUserFullNameException("First name can not be empty");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidUserFullNameException("Last name can not be empty");
        return new FullName(firstName.Trim(), lastName.Trim());
    }
}
