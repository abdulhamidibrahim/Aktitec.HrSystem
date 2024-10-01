using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFileUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confidential",
                table: "FileUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "FileUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Write",
                table: "FileUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Read",
                table: "FileUsers");

            migrationBuilder.DropColumn(
                name: "Write",
                table: "FileUsers");

            migrationBuilder.AddColumn<int>(
                name: "Confidential",
                table: "FileUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
