namespace CommonOperations.Messaging.Events;

public record IntergationEvent
{
    public Guid Id => Guid.NewGuid();
    public  DateTime OccuerdOn => DateTime.UtcNow;
    public string? EventType => GetType().AssemblyQualifiedName;

}
