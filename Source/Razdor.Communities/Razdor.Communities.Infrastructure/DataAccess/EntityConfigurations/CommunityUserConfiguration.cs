using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;

public class CommunityUserConfiguration: IEntityTypeConfiguration<CommunityMember>
{
    public void Configure(EntityTypeBuilder<CommunityMember> builder)
    {
        builder.ToCollection(CollectionNames.CommunityUsers);
        
        builder.Ignore(x => x.DomainEvents);

        builder.HasKey(x => new { UserId = x.Id, x.CommunityId });

        builder.Ignore(x => x.DomainEvents);
        builder.Ignore(x => x.IsTransient);

        builder.Ignore(x => x.RoleIds);
        builder.Property<List<ulong>>("_roleIds")
            .HasElementName(nameof(CommunityMember.RoleIds));

        builder.OwnsOne(x => x.CommunityProfile);
        builder.OwnsOne(x => x.VoiceState, ownedBuilder =>
        {
            ownedBuilder.HasIndex(x => x.ChannelId);
        });
    }
}