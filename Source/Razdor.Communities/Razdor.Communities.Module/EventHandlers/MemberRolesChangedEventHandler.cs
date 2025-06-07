using Mediator;
using Razdor.Communities.Domain.Events;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.PublicEvents.Events;
using Razdor.Shared.IntegrationEvents;

namespace Razdor.Communities.Module.NotificationHandlers;

public class MemberRolesChangedEventHandler(
    IEventBus eventBus    
) : INotificationHandler<MemberRolesChangedEvent>
{
    public async ValueTask Handle(MemberRolesChangedEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.Publish(
            new MemberChangedPublicEvent(
                notification.CommunityId,
                notification.UserId,
                MemberProperties.Roles,
                Roles: notification.Roles
            )
        );
    }
}