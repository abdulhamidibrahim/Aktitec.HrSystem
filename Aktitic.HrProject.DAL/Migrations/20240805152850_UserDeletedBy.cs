
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserDeletedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ApplicationUser");
        }
    }
}
