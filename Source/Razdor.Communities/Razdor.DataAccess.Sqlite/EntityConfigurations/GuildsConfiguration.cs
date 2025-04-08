using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Communities.DataAccess.EF.Entities;

namespace Razdor.Communities.DataAccess.EF.EntityConfigurations;

public class GuildsConfiguration : IEntityTypeConfiguration<Guild>
{
    public void Configure(EntityTypeBuilder<Guild> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}