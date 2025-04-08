using Razdor.Communities.Domain.Guilds;
using Razdor.Communities.Domain.Repositories.Models;
using Razdor.Communities.Domain.Users;

namespace Razdor.Communities.Domain.Repositories;

public interface IGuildsRepository : IRepository<IGuild>
{
    Task<IReadOnlyCollection<IGuild>> GetUserGuilds(IUser user);
    Task<IGuild> CreateGuildAsync(GuildCreationModel model);
    Task<IReadOnlyCollection<IGuild>> getAllAsync();
}