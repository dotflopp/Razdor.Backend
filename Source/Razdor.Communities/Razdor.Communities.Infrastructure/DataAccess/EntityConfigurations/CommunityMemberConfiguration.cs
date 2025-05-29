using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;

public class CommunityMemberConfiguration : IEntityTypeConfiguration<CommunityMember>
{
    public void Configure(EntityTypeBuilder<CommunityMember> builder)
    {
        string[] compositeKey = [nameof(CommunityMember.CommunityId), nameof(CommunityMember.UserId)];

        builder.ToCollection(CollectionNames.CommunityUsers);

        builder.HasKey(compositeKey);

        builder.Ignore(x => x.RoleIds);
        builder.Property<List<ulong>?>("_roleIds")
            .HasElementName(nameof(CommunityMember.RoleIds));

        builder.OwnsOne(x => x.VoiceState, ownsBuilder =>
        {
            ownsBuilder.WithOwner().HasForeignKey(compositeKey);
            ownsBuilder.HasKey(compositeKey);
            ownsBuilder.HasIndex(x => x.ChannelId);
        });
    }
}