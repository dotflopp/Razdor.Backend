using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Razdor.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user-accounts",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    IdentityName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    HashedPassword = table.Column<string>(type: "text", nullable: true),
                    IsOnline = table.Column<bool>(type: "boolean", nullable: false),
                    CredentialsChangeDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SelectedStatus = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    RegistrationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Nickname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user-accounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user-accounts_Email",
                table: "user-accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user-accounts_IdentityName",
                table: "user-accounts",
                column: "IdentityName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user-accounts");
        }
    }
}
