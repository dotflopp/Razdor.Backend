using Mediator;
using Microsoft.AspNetCore.SignalR;
using Razdor.Communities.Module.NotificationHandlers;
using Razdor.Communities.PublicEvents.Events;

namespace Razdor.SignalR.EventHandlers;

public class MemberChangedEventHandler(
    IHubContext<ConnectionHub, IRazdorClient> context,
    ILogger<MemberCreatedNotificationHandler> logger
) : INotificationHandler<MemberChangedPublicEvent>
{

    public async ValueTask Handle(MemberChangedPublicEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{notification}");
        await context.Clients
            .Group(notification.CommunityId.ToString())
            .MemberChanged(notification);   
    }
}