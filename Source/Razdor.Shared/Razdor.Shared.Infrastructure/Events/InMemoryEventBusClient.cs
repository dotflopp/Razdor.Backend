using Mediator;
using Razdor.Shared.IntegrationEvents;
using Razdor.Shared.Module;

namespace Razdor.Shared.Infrastructure.Events;

public class InMemoryEventBusClient(InMemoryEventBus eventBus, IMediator mediator): IEventBus
{
    public async Task Publish<TEvent>(TEvent integrationEvent) where TEvent : IPublicEvent
    {
        await mediator.Publish(integrationEvent);
        await eventBus.Publish(integrationEvent);
    }
    public void Subscribe<TEvent>(PublicEventHandler<TEvent> handler) where TEvent : IPublicEvent
    {
        eventBus.Subscribe(handler);
    }
}