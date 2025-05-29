using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Roles;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;

public class CommunityConfiguration : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        builder.ToCollection(CollectionNames.Communities);

        builder.Ignore(x => x.DomainEvents);

        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.Roles);
        builder.OwnsMany<Role>("_roles",
            ownsBuilder =>
            {
                ownsBuilder.HasKey(x => x.Id);
                ownsBuilder.WithOwner().HasForeignKey(x => x.CommunityId);
                ownsBuilder.HasElementName(nameof(Community.Roles));
            }
        );

        builder.OwnsOne(x => x.Everyone, ownsBuild =>
        {
            ownsBuild.WithOwner().HasForeignKey(nameof(Community.Id));
            ownsBuild.HasKey(nameof(Community.Id));
        });

        builder.HasIndex(x => x.OwnerId);

        builder.Property(x => x.Name)
            .HasMaxLength(Community.NameMaxLength);

        builder.Property(x => x.Description)
            .HasMaxLength(Community.DescriptionMaxLength);
    }
}