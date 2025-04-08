using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Identity.Domain;

namespace Razdor.Identity.DataAccess.EntityConfigurations;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("user-accounts");
        builder.Ignore(x => x.DomainEvents);
        
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.IdentityName)
            .HasMaxLength(UserAccount.MaxIdentityNameLength)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .IsRequired();
            
        builder.HasIndex(x => x.Email)
            .IsUnique(true);

        builder.HasIndex(x => x.IdentityName)
            .IsUnique(true);
    }
}