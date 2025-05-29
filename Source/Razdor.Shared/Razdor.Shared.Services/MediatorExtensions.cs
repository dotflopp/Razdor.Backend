using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Shared.Domain;

namespace Razdor.Shared.Module;

public static class MediatorExtensions
{
    public static Task DispatchDomainEventsAsync(this IMediator mediator, DbContext dbContext)
    {
        return mediator.DispatchDomainEventsAsync<IAggregateRoot>(dbContext);
    }

    public static async Task DispatchDomainEventsAsync<TEntity>(this IMediator mediator, DbContext context)
        where TEntity : class, IAggregateRoot
    {
        var hasEventEntities = context.ChangeTracker
            .Entries<TEntity>()
            .Where(x => x.Entity.DomainEvents.Count > 0)
            .ToList();

        var domainEvents = hasEventEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        foreach (EntityEntry<TEntity> entry in hasEventEntities)
            entry.Entity.ClearDomainEvents();

        foreach (IDomainEvent domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}