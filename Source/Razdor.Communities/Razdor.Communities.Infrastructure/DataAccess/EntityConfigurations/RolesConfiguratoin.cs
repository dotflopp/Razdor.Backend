using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;

public class RolesConfiguratoin: IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToCollection(CollectionNames.Roles);
        
        builder.Ignore(x => x.DomainEvents);

        builder.HasMany<CommunityUser>(CollectionNames.CommunityUsers)
            .WithMany("_roles");

        builder.HasOne<Community>()
            .WithMany("_roles");
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.CommunityId);
    }
}