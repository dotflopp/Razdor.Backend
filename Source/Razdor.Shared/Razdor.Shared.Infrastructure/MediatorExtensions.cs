using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Shared.Domain;

namespace Razdor.Shared.Infrastructure;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEventsAsync<TEntity, TId>(this IMediator mediator, DbContext context)
    {
        var hasEventEntities = context.ChangeTracker
            .Entries<BaseEntity<ISnowflakeId>>()
            .Where(x => x.Entity.DomainEvents.Count > 0)
            .ToList();

        var domainEvents = hasEventEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        foreach (EntityEntry<BaseEntity<ISnowflakeId>> entry in hasEventEntities)
            entry.Entity.ClearDomainEvents();
        
        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}