using Mediator;
using Razdor.Shared.IntegrationEvents;
using Razdor.Shared.Module;

namespace Razdor.Shared.Infrastructure.Events;

public class InMemoryEventBusClient(InMemoryEventBus eventBus, IMediator mediator): IEventBus
{
    public async Task Publish<TEvent>(TEvent integrationEvent) where TEvent : IIntegrationEvent
    {
        await mediator.Publish(integrationEvent);
        await eventBus.Publish(integrationEvent);
    }
    public void Subscribe<TEvent>(IntegrationEventHandleDelegate<TEvent> handler) where TEvent : IIntegrationEvent
    {
        eventBus.Subscribe(handler);
    }
}