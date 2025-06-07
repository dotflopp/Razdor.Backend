using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain;

namespace Razdor.Identity.Infrastructure.DataAccess.EntityConfigurations;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("user-accounts");

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasIndex(x => x.IdentityName)
            .IsUnique();

        builder.Property(x => x.IdentityName)
            .HasMaxLength(UserAccount.MaxIdentityNameLength)
            .IsRequired();
        
        builder.Property(x => x.CredentialsChangeDate)
            .IsRequired();
        
        builder.Property<string>(x => x.Nickname)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(x => x.IsOnline)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.Property(x => x.SelectedStatus)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(x => x.Avatar, ownsBuilder =>
        {
            ownsBuilder.WithOwner().HasForeignKey(nameof(UserAccount.Id));
            ownsBuilder.HasKey(nameof(UserAccount.Id));
            ownsBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Property(x => x.Description)
            .HasMaxLength(UserAccount.MaxDescriptionLength)
            .IsRequired(false);
    }
}