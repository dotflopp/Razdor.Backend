namespace Razdor.Shared.Entities;

public class BaseEntity<T>(
    T? id
) where T : ISnowflakeId
{
    private List<IDomainEvent>? _domainEvents = null;

    public T? Id { get; protected set; } = id;

    public IReadOnlyCollection<IDomainEvent> DomainEvents =>
        _domainEvents?.AsReadOnly() ?? Array.Empty<IDomainEvent>().AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public bool IsTransient()
    {
        return Id is null;
    }
}