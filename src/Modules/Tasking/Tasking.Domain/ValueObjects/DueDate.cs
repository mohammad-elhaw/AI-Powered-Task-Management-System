namespace Tasking.Domain.ValueObjects;

public record DueDate
{
    public DateTime Value { get; }
    private DueDate() { }
    private DueDate(DateTime value)
    {
        Value = value.Date;
    }

    public static DueDate Create(DateTime dueDate)
    {
        if (dueDate < DateTime.UtcNow.Date)
        {
            // we will replace with result pattern later
            throw new ArgumentException("Due date cannot be in the past.");
        }
        return new DueDate(dueDate);
    }

}
