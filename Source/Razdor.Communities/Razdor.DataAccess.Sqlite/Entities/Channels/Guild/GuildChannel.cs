using Razdor.Communities.Domain.Channels.Guild;

namespace Razdor.Communities.DataAccess.EF.Entities.Channels.Guild;

public abstract class GuildChannel : BaseChannel, IGuildChannel
{
    public required string Name { get; init; }
    public required ulong GuildId { get; init; }
    public required uint Position { get; init; }
    
    internal Entities.Guild? Guild { get; set; } = null;
}