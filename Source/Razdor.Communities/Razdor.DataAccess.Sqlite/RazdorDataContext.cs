using System.Threading.Channels;

using Microsoft.EntityFrameworkCore;

using Razdor.DataAccess.EntityFramework.Entities;
using Razdor.DataAccess.EntityFramework.Entities.Channels;
using Razdor.DataAccess.EntityFramework.Entities.Channels.Guild;
using Razdor.DataAccess.EntityFramework.EntityConfigurations;

namespace Razdor.DataAccess.EntityFramework;

public class RazdorDataContext : DbContext
{
    public RazdorDataContext(DbContextOptions options) : base(options)
    {
        Guilds = Set<Guild>();
        GuildChannels = Set<GuildChannel>();
        GuildVoiceChannels = Set<GuildVoiceChannel>();
    }

    internal DbSet<User> Users { get; }
    internal DbSet<BaseChannel> Channels { get; }
    internal DbSet<GuildChannel> GuildChannels { get; }
    internal DbSet<GuildVoiceChannel> GuildVoiceChannels { get; }
    internal DbSet<Guild> Guilds { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChannelsConfiguration());
        modelBuilder.ApplyConfiguration(new GuildChannelsConfiguration());
    }
}
