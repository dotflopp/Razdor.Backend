﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razdor.Identity.Domain.Users;

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

        builder.Ignore(x => x.DomainEvents);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.IdentityName)
            .HasMaxLength(UserAccount.MaxIdentityNameLength)
            .IsRequired();

        builder.Property(x => x.CredentialsChangeDate)
            .IsRequired();

        builder.Ignore(x => x.Nickname);
        builder.Property<string>("_nickname")
            .HasColumnName(nameof(UserAccount.Nickname))
            .HasMaxLength(UserAccount.MaxNicknameLength);

        builder.Property(x => x.Email)
            .IsRequired();

        builder.Property(x => x.IsOnline)
            .IsRequired();

        builder.Property(x => x.SelectedStatus)
            .IsRequired();
        builder.Ignore(x => x.DisplayedStatus);

        builder.Property(x => x.Description)
            .HasMaxLength(UserAccount.MaxDescriptionLength)
            .IsRequired(false);
    }
}