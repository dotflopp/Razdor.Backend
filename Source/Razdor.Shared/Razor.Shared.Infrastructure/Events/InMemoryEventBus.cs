using Razdor.Shared.Module;

namespace Razor.Shared.Infrastructure;



public class InMemoryEventBus: IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _eventHandlers = new();
    
    public async Task Publish<TEvent>(TEvent integrationEvent) where TEvent : IIntegrationEvent
    {
        Type eventType = integrationEvent.GetType();
        if (!_eventHandlers.TryGetValue(eventType, out var handlers))
            return;

        foreach (Delegate handler in handlers)
        {
            if (handler is Func<TEvent, ValueTask> functionHandler)
                await functionHandler.Invoke(integrationEvent);
        }
    }
    public void Subscribe<TEvent>(IntegrationEventHandleDelegate<TEvent> handler) where TEvent : IIntegrationEvent
    {
        Type eventType = typeof(TEvent);

        if (_eventHandlers.TryGetValue(eventType, out var handlers))
            handlers.Add(handler);
        else 
            _eventHandlers.Add(eventType, [handler]);
    }
}