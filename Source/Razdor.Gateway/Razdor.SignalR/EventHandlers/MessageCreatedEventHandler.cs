using Mediator;
using Microsoft.AspNetCore.SignalR;
using Razdor.Communities.Module.NotificationHandlers;
using Razdor.Messages.Domain.Events;
using Razdor.Messages.PublicEvents;
using Razdor.Messages.PublicEvents.Events;

namespace Razdor.SignalR.EventHandlers;

public class MessageCreatedEventHandler(
    IHubContext<ConnectionHub, IRazdorClient> context,
    ILogger<MessageCreatedEventHandler> logger
) : INotificationHandler<MessageCreatedPublicEvent>
{
    public async ValueTask Handle(MessageCreatedPublicEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{notification}");

        await context.Clients
            .Group(notification.ChannelId.ToString())
            .MessageCreated(notification.Message);
    }
}