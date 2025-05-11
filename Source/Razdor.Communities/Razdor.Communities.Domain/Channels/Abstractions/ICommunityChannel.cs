using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels.Abstractions;

public interface ICommunityChannel: 
    INamed, 
    ISnowflakeEntity, 
    ICommunityEntity<ulong>, 
    IOverwritesPermission
{
    ChannelType Type { get; }

    /// <summary>
    /// Позиция канала в категории
    /// </summary>
    uint Position { get; }
}

public enum ChannelType
{
    Category,
    Message,
    Voice,
    Fork,
}
