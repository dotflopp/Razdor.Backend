using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Domain;

public interface IGuildChannel : IChannel, ISnowflakeEntity, IEntity<ulong>
{
    
}