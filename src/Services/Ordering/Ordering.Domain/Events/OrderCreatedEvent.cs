namespace Ordering.Domain.Events;

public record class OrderCreatedEvent(Order order) : IDomainEvent;
