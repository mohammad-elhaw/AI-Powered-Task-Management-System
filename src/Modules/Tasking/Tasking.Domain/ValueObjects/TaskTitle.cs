namespace Tasking.Domain.ValueObjects;

public record TaskTitle
{
    public string Value { get; }

    private TaskTitle() { }
    private TaskTitle(string value)
    {
        Value = value;
    }

    public static TaskTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            // we will replace this with result pattern later
            throw new ArgumentException("Task title cannot be empty.", nameof(value));
        }
        if (value.Length > 100)
        {
            // we will replace this with result pattern later
            throw new ArgumentException("Task title cannot exceed 100 characters.", nameof(value));
        }
        return new TaskTitle(value);
    }
}
