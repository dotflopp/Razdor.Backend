using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

public interface IChannel : ISnowflakeEntity, IEntity<ulong>
{
    /// <summary>
    /// Идентификатор CategoryChannel либо родительского MessageChannel для ForkChannel
    /// </summary>
    ulong ParentId { get; }
    string Name { get; }
}