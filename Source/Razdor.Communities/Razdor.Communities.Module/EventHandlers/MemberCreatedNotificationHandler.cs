using Mediator;
using Razdor.Communities.Domain.Events;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Module.Services.Members;
using Razdor.Communities.PublicEvents.Events;
using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.Shared.IntegrationEvents;

namespace Razdor.Communities.Module.NotificationHandlers;

public class MemberCreatedNotificationHandler(
    IEventBus eventBus,
    ICommunityUserDataAccessor userDataAccessor
) : INotificationHandler<CommunityMemberAddedEvent>
{
    public async ValueTask Handle(CommunityMemberAddedEvent notification, CancellationToken cancellationToken)
    {
        UserDataViewModel userData = await userDataAccessor.FillAsync(notification.Member.UserId, new MemberProfile(null,null));
        await eventBus.Publish(
            new CommunityMemberAddedPublicEvent(
                CommunityMemberPreviewModel.From(notification.Member, userData)
            )
        );
    }
}