using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;

public class EveryoneRoleConfiguration: IEntityTypeConfiguration<EveryoneRole>
{
    public void Configure(EntityTypeBuilder<EveryoneRole> builder)
    {
        builder.ToCollection(CollectionNames.Roles);
        
        builder.Ignore(x => x.DomainEvents);

        builder.HasBaseType<Role>();
        
        builder.HasMany<Community>();
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.CommunityId);

        builder.Ignore(x => x.Name);
        builder.Ignore(x => x.IsMentioned);
    }
}