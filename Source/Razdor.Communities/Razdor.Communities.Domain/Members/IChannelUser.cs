using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

/// <summary>
/// Планируется как кэш
/// </summary>
public interface IChannelUser : ISnowflakeEntity, IEntity<ulong>
{
    DateTimeOffset UpdateDateTime { get; }
    ulong ChannelId { get; }
    /// <summary>
    /// Заранее просчитанные права пользователя для канала
    /// </summary>
    UserPermissions ChannelPermissions { get; }
}