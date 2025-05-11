using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;

public class CommunityConfiguration : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        builder.ToCollection(CollectionNames.Communities);

        builder.Ignore(x => x.DomainEvents);
        
        builder.HasKey(x => x.Id);

        // builder.Ignore(x => x.Roles);
        // builder.Property<List<Role>>("_roles")
        //     .HasElementName(nameof(Community.Roles));
        
        builder.HasMany<Role>("_roles")
            .WithOne()
            .HasForeignKey(x => x.CommunityId);

        builder.HasIndex(x => x.OwnerId);

        builder.Property(x => x.Name)
            .HasMaxLength(Community.NameMaxLength);

        builder.Property(x => x.Description)
            .HasMaxLength(Community.DescriptionMaxLength);
    }
}