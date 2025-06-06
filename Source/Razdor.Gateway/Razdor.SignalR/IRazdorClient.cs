using Razdor.Communities.PublicEvents.Events;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.Identity.PublicEvents.Event;
using Razdor.Messages.PublicEvents.ViewModels;

namespace Razdor.SignalR;

public interface IRazdorClient
{
    Task MessageCreated(MessageViewModel message);
    Task ChannelCreated(ChannelViewModel channel);
    Task CommunityMemberAdded(CommunityMemberPreviewModel member);
    Task MemberChanged(MemberChangedPublicEvent notification);
    Task UserChanged(UserChangedPublicEvent notification);
}