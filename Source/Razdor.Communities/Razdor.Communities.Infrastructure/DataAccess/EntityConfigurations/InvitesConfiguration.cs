using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain.Invites;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;

public class InvitesConfiguration : IEntityTypeConfiguration<Invite>
{
    public void Configure(EntityTypeBuilder<Invite> builder)
    {
        builder.ToCollection("invites");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CommunityId);
        builder.HasIndex(x => x.CreatorId);
    }
}