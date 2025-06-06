using Mediator;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Events;
using Razdor.Communities.PublicEvents.Events;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Shared.IntegrationEvents;

namespace Razdor.Communities.Module.NotificationHandlers;

public class ChannelCreatedNotificationHandler(
    IEventBus eventBus    
) : INotificationHandler<ChannelCreatedEvent>
{
    public async ValueTask Handle(ChannelCreatedEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.Publish(new ChannelCreatedPublicEvent(
            ChannelViewModel.From(notification.Channel)   
        ));
    }
}