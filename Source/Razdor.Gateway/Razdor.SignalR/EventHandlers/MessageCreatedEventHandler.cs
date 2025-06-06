using Mediator;
using Microsoft.AspNetCore.SignalR;
using Razdor.Messages.Domain.Events;
using Razdor.Messages.PublicEvents;
using Razdor.Messages.PublicEvents.Events;

namespace Razdor.SignalR.EventHandlers;

public class MessageCreatedEventHandler(
    IHubContext<ConnectionHub, IRazdorClient> context
) : INotificationHandler<MessageCreatedPublicEvent>
{
    public async ValueTask Handle(MessageCreatedPublicEvent notification, CancellationToken cancellationToken)
    {
        await context.Clients
            .Group(notification.ChannelId.ToString())
            .MessageCreated(notification.Message);
    }
}