using Razdor.Communities.Domain.Channels;
using Razdor.Shared.IntegrationEvents;

namespace Razdor.Communities.PublicEvents;

public interface IChannelEvent : IPublicEvent
{
    public ChannelType Type { get; set; }
    public ulong ChannelId { get; set; }
}