namespace Razdor.Shared.IntegrationEvents;

public delegate ValueTask PublicEventHandler<TEvent>(TEvent integrationEvent);

public interface IEventBus
{
    Task Publish<TEvent>(TEvent integrationEvent)
        where TEvent : IPublicEvent;

    void Subscribe<TEvent>(PublicEventHandler<TEvent> handler)
        where TEvent : IPublicEvent;
}