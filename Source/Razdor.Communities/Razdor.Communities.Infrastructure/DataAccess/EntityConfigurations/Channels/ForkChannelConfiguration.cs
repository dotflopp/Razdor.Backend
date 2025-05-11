using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Communities.Domain.Channels;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

public class ForkChannelConfiguration : IEntityTypeConfiguration<ForkChannel>
{
    public void Configure(EntityTypeBuilder<ForkChannel> builder)
    {
        var communityChannel = new CommunityChannelConfiguration<ForkChannel>();
        communityChannel.Configure(builder);
    }
}