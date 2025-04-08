namespace Razdor.Shared.Domain;

public interface IEntity
{
    ulong Id { get; }
    bool IsTransient { get; }
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}