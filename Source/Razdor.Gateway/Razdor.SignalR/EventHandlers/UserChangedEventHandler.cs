﻿using Mediator;
using Microsoft.AspNetCore.SignalR;
using Razdor.Identity.Domain.Events;
using Razdor.Identity.PublicEvents.Event;

namespace Razdor.SignalR.EventHandlers;

public class UserChangedEventHandler(
    IHubContext<ConnectionHub, IRazdorClient> context,
    ILogger<UserChangedEventHandler> logger
) : INotificationHandler<UserChangedPublicEvent>
{
    public async ValueTask Handle(UserChangedPublicEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{notification}");
        await context.Clients
            .Group(notification.UserId.ToString())
            .UserChanged(notification);   
    }
}