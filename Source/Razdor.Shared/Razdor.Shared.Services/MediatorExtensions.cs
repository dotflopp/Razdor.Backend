using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Shared.Domain;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Shared.Module;

public static class MediatorExtensions
{
    public static Task DispatchDomainEventsAsync(this IMediator mediator, DbContext dbContext)
        => mediator.DispatchDomainEventsAsync<IAggregateRoot>(dbContext);

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

        foreach (var entry in hasEventEntities)
            entry.Entity.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}