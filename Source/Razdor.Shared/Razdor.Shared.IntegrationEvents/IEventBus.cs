namespace Razdor.Shared.IntegrationEvents;

public delegate ValueTask IntegrationEventHandleDelegate<TEvent>(TEvent integrationEvent);

public interface IEventBus
{
    Task Publish<TEvent>(TEvent integrationEvent)
        where TEvent : IIntegrationEvent;

    void Subscribe<TEvent>(IntegrationEventHandleDelegate<TEvent> handler)
        where TEvent : IIntegrationEvent;
}