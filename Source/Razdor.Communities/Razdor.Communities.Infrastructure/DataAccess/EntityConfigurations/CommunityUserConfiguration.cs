using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;

public record UserRole(
    ulong RoleId,
    ulong UserId,
    CommunityUser User,
    Role Role
);

public class CommunityUserConfiguration: IEntityTypeConfiguration<CommunityUser>
{
    public void Configure(EntityTypeBuilder<CommunityUser> builder)
    {
        builder.ToCollection(CollectionNames.CommunityUsers);
        
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CommunityId);

        builder.Ignore(x => x.Roles);
        builder.Property<List<Role>>("_roles")
            .HasElementName(nameof(CommunityUser.Roles));
        
        builder.HasMany<Role>("_roles")
            .WithMany()
            .UsingEntity<UserRole>(
                left => left.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                right => right.HasOne(x=>x.User).WithMany().HasForeignKey(x => x.UserId),
                join => join.HasKey(x => new {x.UserId, x.RoleId} )
            );
    }
}