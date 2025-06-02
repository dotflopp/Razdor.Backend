using Mediator;
using Razdor.Communities.Domain.Channels;

namespace Razdor.Shared.Module.Authorization;

public interface IRequiredChannelType : IMessage
{
    public ulong ChannelId { get; }
    public ChannelType AllowedTypes { get; }
}