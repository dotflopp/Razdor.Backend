using Microsoft.EntityFrameworkCore;
using Razdor.Communities.DataAccess.EF.Entities;
using Razdor.Communities.DataAccess.EF.Entities.Channels;
using Razdor.Communities.DataAccess.EF.Entities.Channels.Guild;
using Razdor.Communities.DataAccess.EF.EntityConfigurations;

namespace Razdor.Communities.DataAccess.EF;

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
