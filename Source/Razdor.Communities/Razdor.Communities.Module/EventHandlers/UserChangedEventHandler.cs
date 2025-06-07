using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.PublicEvents.Events;
using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.Identity.Domain.Events;
using Razdor.Identity.PublicEvents.Event;
using Razdor.Shared.IntegrationEvents;

namespace Razdor.Communities.Module.NotificationHandlers;

public class UserChangedEventHandler(
    CommunitiesDbContext context,
    IEventBus eventBus
): INotificationHandler<UserChangedPublicEvent>
{
    public async ValueTask Handle(UserChangedPublicEvent notification, CancellationToken cancellationToken)
    {
        MemberProperties changes = (MemberProperties)notification.Changes & MemberProperties.UserProperties;
        if (changes == 0)
            return;
        
        var communityMembers = await context.CommunityMembers.AsNoTracking()
            .Where(x => x.UserId == notification.UserId)
            .Select(x => new { x.CommunityId, x.UserId })
            .ToListAsync(cancellationToken);

        
        foreach (var communityMember in communityMembers)
        {
            await eventBus.Publish(new MemberChangedPublicEvent(
                communityMember.CommunityId,
                notification.UserId,
                changes,
                (CommunicationStatus?)notification.Status,
                notification.Nickname,
                notification.Avatar,
                notification.Description
            ));
        }
    }
}