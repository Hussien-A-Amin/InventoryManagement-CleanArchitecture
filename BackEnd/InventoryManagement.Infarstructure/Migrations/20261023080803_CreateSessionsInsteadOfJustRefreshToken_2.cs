using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Infarstructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateSessionsInsteadOfJustRefreshToken_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "ApplicationSessions");

            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                table: "ApplicationSessions",
                newName: "IsCanceled");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCanceled",
                table: "ApplicationSessions",
                newName: "IsRevoked");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "ApplicationSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
