using System.Collections.ObjectModel;

namespace Razdor.Shared.Domain;

public class BaseAggregateRoot : IAggregateRoot
{
    private List<IDomainEvent>? _domainEvents;

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