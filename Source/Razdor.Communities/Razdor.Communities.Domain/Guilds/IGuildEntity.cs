namespace Razdor.Communities.Domain.Guilds;

public interface IGuildEntity : IEntity
{
    ulong GuildId { get; }
}