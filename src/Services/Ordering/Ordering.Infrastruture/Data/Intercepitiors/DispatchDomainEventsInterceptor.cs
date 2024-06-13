using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastruture.Data.Intercepitiors;
    public  class DispatchDomainEventsInterceptor(IMediator mediator) 
    : SaveChangesInterceptor
    {
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult(); 
        return base.SavingChanges(eventData, result);
    }
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var aggrates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(a => a.Entity.DomainEvents.Any())
            .Select(a => a.Entity);

        var domainEvents = aggrates
            .SelectMany(a => a.DomainEvents)
            .ToList(); 

        aggrates.ToList().ForEach(a => a.ClearDomainEvents());

        foreach ( var domainEvent in domainEvents) 
            await mediator.Publish(domainEvent);
    }
}

