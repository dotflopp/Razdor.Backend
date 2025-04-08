namespace Razdor.Communities.Domain.Guilds;

public interface IGuild : IEntity, INamed
{
    string? Icon { get; }
}