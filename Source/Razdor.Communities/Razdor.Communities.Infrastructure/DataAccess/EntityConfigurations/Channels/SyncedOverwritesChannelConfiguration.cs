using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

public class SyncedOverwritesChannelConfiguration<TChannel>: IEntityTypeConfiguration<TChannel>
    where TChannel : SyncedOverwritesChannel
{
    public void Configure(EntityTypeBuilder<TChannel> builder)
    {
        var communityChannel = new CommunityChannelConfiguration<TChannel>();
        communityChannel.Configure(builder);
        
        builder.Property<List<Overwrite>>("_overwrites")
            .HasElementName(nameof(SyncedOverwritesChannel.Overwrites));
    }
}