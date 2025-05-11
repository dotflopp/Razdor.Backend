using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Communities.Domain.Channels;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

public class VoiceChannelConfiguration: IEntityTypeConfiguration<VoiceChannel>
{
    public void Configure(EntityTypeBuilder<VoiceChannel> builder)
    {
        var syncedChannelConfiguration = new SyncedOverwritesChannelConfiguration<VoiceChannel>();
        syncedChannelConfiguration.Configure(builder);
    }
}