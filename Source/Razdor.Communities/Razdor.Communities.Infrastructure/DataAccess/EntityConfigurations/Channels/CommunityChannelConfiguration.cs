using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain.Channels.Abstractions;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

public class CommunityChannelConfiguration<TChannel>: IEntityTypeConfiguration<TChannel>
    where TChannel : CommunityChannel
{
    public void Configure(EntityTypeBuilder<TChannel> builder)
    {
        builder.ToCollection(CollectionNames.Channels);
        
        builder.Ignore(x => x.DomainEvents);

        builder.Ignore(x => x.Overwrites);

        builder.HasBaseType<CommunityChannel>();
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.CommunityId);

    }
}