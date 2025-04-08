using Razdor.Communities.DataAccess.EF.Entities.Channels.Guild;
using Razdor.Communities.Domain.Guilds;

namespace Razdor.Communities.DataAccess.EF.Entities;

public class Guild : IGuild
{
    public required EntityId Id { get; init; }
    public required string Name { get; init; }
    public required string? Icon { get; init; }
    
    internal IReadOnlyCollection<User>? Users { get; set; }
    internal IReadOnlyCollection<GuildChannel>? Channels { get; set; }
}
