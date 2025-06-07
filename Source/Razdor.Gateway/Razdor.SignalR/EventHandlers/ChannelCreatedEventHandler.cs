using Mediator;
using Microsoft.AspNetCore.SignalR;
using Razdor.Communities.PublicEvents.Events;

namespace Razdor.SignalR.EventHandlers;

public class ChannelCreatedEventHandler(
    IHubContext<ConnectionHub, IRazdorClient> context,
    ILogger<ChannelCreatedEventHandler> logger
): INotificationHandler<ChannelCreatedPublicEvent>
{
    public async ValueTask Handle(ChannelCreatedPublicEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{notification}");
        await context.Clients
            .Group(notification.CommunityId.ToString())
            .ChannelCreated(notification.Channel);   
    }
}