using Razdor.Communities.Domain.Channels;

namespace Razdor.Communities.DataAccess.EF.Entities.Channels;

public abstract class BaseChannel : IChannel
{
    public required EntityId Id { get; init; }
    public required ChannelType Type { get; init; }
}