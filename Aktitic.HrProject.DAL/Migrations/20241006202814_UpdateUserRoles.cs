using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CompanyRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "ApplicationUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRoles_UserId",
                table: "CompanyRoles",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyRoles_ApplicationUser_UserId",
                table: "CompanyRoles",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyRoles_ApplicationUser_UserId",
                table: "CompanyRoles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyRoles_UserId",
                table: "CompanyRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CompanyRoles");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
