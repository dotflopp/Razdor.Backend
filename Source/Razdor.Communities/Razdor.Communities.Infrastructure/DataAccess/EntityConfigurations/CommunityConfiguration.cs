using System.Net.NetworkInformation;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
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

        builder.Ignore(x => x.Roles);
        builder.OwnsMany<Role>("_roles",
            ownedBuilder => {
                ownedBuilder.HasKey(x => x.Id);
                ownedBuilder.WithOwner().HasForeignKey(x => x.CommunityId);
                ownedBuilder.HasElementName(nameof(Community.Roles));
            }
        );
        
        builder.HasIndex(x => x.OwnerId);
        
        builder.Property(x => x.Name)
            .HasMaxLength(Community.NameMaxLength);

        builder.Property(x => x.Avatar);
        
        builder.Property(x => x.Description)
            .HasMaxLength(Community.DescriptionMaxLength);

        builder.Property(x => x.DefaultNotificationPolicy);
    }
}