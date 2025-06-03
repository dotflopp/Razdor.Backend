using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Razdor.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "user-accounts");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "user-accounts",
                newName: "Avatar_SourceUrl");

            migrationBuilder.AddColumn<string>(
                name: "Avatar_FileName",
                table: "user-accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar_MediaType",
                table: "user-accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Avatar_Size",
                table: "user-accounts",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar_FileName",
                table: "user-accounts");

            migrationBuilder.DropColumn(
                name: "Avatar_MediaType",
                table: "user-accounts");

            migrationBuilder.DropColumn(
                name: "Avatar_Size",
                table: "user-accounts");

            migrationBuilder.RenameColumn(
                name: "Avatar_SourceUrl",
                table: "user-accounts",
                newName: "Avatar");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "user-accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
