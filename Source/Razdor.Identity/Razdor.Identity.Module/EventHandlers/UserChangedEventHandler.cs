using Mediator;
using Razdor.Identity.Domain.Events;
using Razdor.Identity.PublicEvents.Event;
using Razdor.Shared.IntegrationEvents;

namespace Razdor.Identity.Module.EventHandlers;

public class UserChangedEventHandler(
    IEventBus eventBus   
) : INotificationHandler<UserChangedEvent>
{
    public async ValueTask Handle(UserChangedEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.Publish(UserChangedPublicEvent.From(notification));
    }
}