using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Domain;

namespace Razdor.DataAccess.EntityFramework.Entities.Channels;

public abstract class BaseChannel : IChannel
{
    public required EntityId Id { get; init; }
    public required ChannelType Type { get; init; }
}