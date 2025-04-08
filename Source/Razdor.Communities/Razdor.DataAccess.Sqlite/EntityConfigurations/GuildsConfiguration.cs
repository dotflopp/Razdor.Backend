using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.DataAccess.EntityFramework.Entities;

namespace Razdor.DataAccess.EntityFramework.EntityConfigurations;

public class GuildsConfiguration : IEntityTypeConfiguration<Guild>
{
    public void Configure(EntityTypeBuilder<Guild> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}