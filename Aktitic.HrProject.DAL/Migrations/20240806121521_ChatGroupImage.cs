using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChatGroupImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "ChatGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ChatGroups");
        }
    }
}
