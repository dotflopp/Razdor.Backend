using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Communities.DataAccess.EF.Entities;
using Razdor.Communities.Domain.Guilds;
using Razdor.Communities.Domain.Repositories;
using Razdor.Communities.Domain.Repositories.Models;
using Razdor.Communities.Domain.Users;

namespace Razdor.Communities.DataAccess.EF.Repositories;

public class GuildsRepository : IGuildsRepository
{
    private readonly RazdorDataContext _context;

    public GuildsRepository(RazdorDataContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<IGuild>> GetUserGuilds(IUser user)
    {
        if (user is User efUser && efUser.Guilds != null) 
        {
            return efUser.Guilds;
        }

        return await _context.Guilds
            .Where(guild => guild.Users!.Any(guildUser => guildUser.Id == user.Id))
            .ToListAsync();
    }

    public async Task<IGuild> CreateGuildAsync(GuildCreationModel model)
    {
        Guild guild = new Guild()
        {
            Id = 0,
            Name = model.Name,
            Icon = model.Icon,
        };
        EntityEntry<Guild> result = await _context.Guilds.AddAsync(guild);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<IReadOnlyCollection<IGuild>> getAllAsync()
    {
        return await _context.Guilds.ToListAsync();
    }
}