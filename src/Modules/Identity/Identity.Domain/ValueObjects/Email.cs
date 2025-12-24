using Identity.Domain.Exceptions.User;
using System.Text.RegularExpressions;

namespace Identity.Domain.ValueObjects;

public record Email
{
    public string Value { get; }
    private Email(string value) => Value = value;
    
    public static Email Create(string email)
    {
        if(string.IsNullOrEmpty(email))
            throw new InvalidUserEmailException("email can not be empty");

        string trimmedEmail = email.Trim();

        if (!Regex.IsMatch(trimmedEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new InvalidUserEmailException("Invalid Email Fromat");

        return new Email(trimmedEmail.ToLowerInvariant());
    }
}
