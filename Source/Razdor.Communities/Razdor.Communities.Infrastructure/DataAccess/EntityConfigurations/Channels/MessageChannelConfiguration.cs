using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Communities.Domain.Channels;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

public class MessageChannelConfiguration: IEntityTypeConfiguration<MessageChannel>
{
    public void Configure(EntityTypeBuilder<MessageChannel> builder)
    {
        var communityChannel = new CommunityChannelConfiguration<MessageChannel>();
        communityChannel.Configure(builder);
    }
}