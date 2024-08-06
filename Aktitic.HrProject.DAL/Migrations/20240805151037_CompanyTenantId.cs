using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CompanyTenantId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Companies_CompanyId",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ApplicationUser",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_CompanyId",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Companies_TenantId",
                table: "ApplicationUser",
                column: "TenantId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Companies_TenantId",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "ApplicationUser",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_TenantId",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Companies_CompanyId",
                table: "ApplicationUser",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
