using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Communities.DataAccess.EF.Entities;
using Razdor.Communities.DataAccess.EF.Entities.Channels;
using Razdor.Communities.DataAccess.EF.Entities.Channels.Guild;
using Razdor.Communities.Domain.Channels;

namespace Razdor.Communities.DataAccess.EF.EntityConfigurations;

public class ChannelsConfiguration : IEntityTypeConfiguration<BaseChannel>
{
    public void Configure(EntityTypeBuilder<BaseChannel> builder)
    {
        builder
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
        builder.HasDiscriminator(channel => channel.Type)
            .HasValue<GuildVoiceChannel>(ChannelType.GuildVoiceChannel);
    }
}

public class GuildChannelsConfiguration : IEntityTypeConfiguration<GuildChannel>
{
    public void Configure(EntityTypeBuilder<GuildChannel> builder)
    {
        builder.HasOne<Guild>(channel => channel.Guild)
            .WithMany(guild => guild.Channels)
            .HasForeignKey(channel => channel.GuildId)
            .IsRequired();
    }
}