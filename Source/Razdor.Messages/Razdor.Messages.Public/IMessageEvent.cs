using Razdor.Shared.IntegrationEvents;

namespace Razdor.Messages.PublicEvents;

public interface IMessageEvent : IPublicEvent
{
    ulong ChannelId { get; }
}
