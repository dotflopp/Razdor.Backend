namespace Razdor.Shared.Domain;

public interface IEntity
{
    ulong Id { get; }
    bool IsTransient { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}