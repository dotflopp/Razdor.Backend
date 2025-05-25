using System.Collections.ObjectModel;

namespace Razdor.Shared.Domain;

public abstract class BaseSnowflakeEntity(
    ulong id
) : BaseAggregateRoot, IAggregateRoot, IEntity<ulong>
{
    public ulong Id { get; protected set; } = id;
    public bool IsTransient => Id == 0;
}