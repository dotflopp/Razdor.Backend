using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.Shared.IntegrationEvents;

namespace Razdor.Communities.PublicEvents.Events;

public record CommunityMemberAddedPublicEvent(
    CommunityMemberPreviewModel Member
) : ICommunityEvent, IPublicEvent
{
    public ulong CommunityId => Member.CommunityId;
}