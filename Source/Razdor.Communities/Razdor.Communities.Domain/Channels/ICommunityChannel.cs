using Razdor.Communities.Domain.Communities;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

public interface ICommunityChannel : IChannel, ISnowflakeEntity, IEntity<ulong>
{
    ICommunity Community { get; }
    /// <summary>
    /// CategoryChannel либо родительского MessageChannel для ForkChannel
    /// </summary>
    IChannel? Parent { get; }
    /// <summary>
    /// Позиция канала в категории
    /// </summary>
    uint Position { get; }
    /// <summary>
    /// Указывает на то что права синхронизируются с родительским каналом
    /// </summary>
    bool IsSyncing { get; }
}