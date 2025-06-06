﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Razdor.Identity.Infrastructure.DataAccess;

#nullable disable

namespace Razdor.Identity.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityPostgresDbContext))]
    partial class IdentityPostgresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Razdor.Identity.Domain.Users.UserAccount", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset>("CredentialsChangeDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("text");

                    b.Property<string>("IdentityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("boolean");

                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SelectedStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IdentityName")
                        .IsUnique();

                    b.ToTable("user-accounts", (string)null);
                });

            modelBuilder.Entity("Razdor.Identity.Domain.Users.UserAccount", b =>
                {
                    b.OwnsOne("Razdor.Shared.Domain.MediaFileMeta", "Avatar", b1 =>
                        {
                            b1.Property<decimal>("Id")
                                .HasColumnType("numeric(20,0)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("MediaType")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<long>("Size")
                                .HasColumnType("bigint");

                            b1.Property<string>("SourceUrl")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("Id");

                            b1.ToTable("user-accounts");

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.Navigation("Avatar");
                });
#pragma warning restore 612, 618
        }
    }
}
