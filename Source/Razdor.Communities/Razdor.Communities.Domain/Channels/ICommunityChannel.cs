using Razdor.Communities.Domain.Communities;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

public interface ICommunityChannel : IChannel, ISnowflakeEntity, IEntity<ulong>
{
    ICommunity Community { get; }
    ulong CommunityId { get; }
    uint Position { get; }
    bool PermissionsIsSyncing { get; }
}