using Mediator;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Shared.IntegrationEvents;

namespace Razdor.Communities.PublicEvents.Events;

public record ChannelCreatedPublicEvent(
    ChannelViewModel Channel
) : ICommunityEvent, IPublicEvent<ChannelViewModel>
{
    public ulong CommunityId => Channel.CommunityId;
    public ChannelViewModel Payload => Channel;
};