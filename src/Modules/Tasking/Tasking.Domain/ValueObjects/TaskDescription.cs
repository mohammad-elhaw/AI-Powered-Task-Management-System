namespace Tasking.Domain.ValueObjects;

public record TaskDescription
{
    public string Value { get; }
    private TaskDescription() { }
    private TaskDescription(string value)
    {
        Value = value;
    }

    public static TaskDescription Create(string description)
        => new (description ?? string.Empty);
}
