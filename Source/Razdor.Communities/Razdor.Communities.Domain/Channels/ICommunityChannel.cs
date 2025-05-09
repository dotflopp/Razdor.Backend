using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

public interface ICommunityChannel: 
    INamed, 
    ISnowflakeEntity, 
    ICommunityEntity<ulong>, 
    IOverwritesPermission
{
    /// <summary>
    /// Позиция канала в категории
    /// </summary>
    uint Position { get; }
}
