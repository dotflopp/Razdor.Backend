using Razdor.Communities.Domain.Channels;
using Razdor.Messages.PublicEvents.ViewModels;

namespace Razdor.Messages.PublicEvents.Events;

public record MessageCreatedPublicEvent(
    MessageViewModel Message
) : IMessageEvent
{
    public ulong ChannelId => Message.ChannelId;
};