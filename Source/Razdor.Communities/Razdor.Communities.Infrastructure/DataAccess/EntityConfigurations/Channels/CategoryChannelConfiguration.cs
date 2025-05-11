using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Communities.Domain.Channels;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

public class CategoryChannelConfiguration: IEntityTypeConfiguration<CategoryChannel>
{
    public void Configure(EntityTypeBuilder<CategoryChannel> builder)
    {
        var syncedChannelConfiguration = new SyncedOverwritesChannelConfiguration<CategoryChannel>();
        syncedChannelConfiguration.Configure(builder);

        builder.Ignore(x => x.Parent);
    }
}