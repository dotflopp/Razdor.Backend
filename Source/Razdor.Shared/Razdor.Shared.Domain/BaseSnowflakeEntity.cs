using System.Collections.ObjectModel;

namespace Razdor.Shared.Domain;

public abstract class BaseAggregateRootEntity<TId>(
    TId id
) : IAggregateRoot, IEntity<TId>
{
    private List<IDomainEvent>? _domainEvents;
    public TId Id { get; protected set; } = id;
    public bool IsTransient => Id is null;

    public IReadOnlyCollection<IDomainEvent> DomainEvents =>
        _domainEvents?.AsReadOnly() ?? ReadOnlyCollection<IDomainEvent>.Empty;

    public virtual void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }

    public virtual void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    public virtual void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}