using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CompanyUsersRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_CompanyId",
                table: "ApplicationUser");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ManagerId",
                table: "Companies",
                column: "ManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_CompanyId",
                table: "ApplicationUser",
                column: "tenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ApplicationUser_ManagerId",
                table: "Companies",
                column: "ManagerId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ApplicationUser_ManagerId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ManagerId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_CompanyId",
                table: "ApplicationUser");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_CompanyId",
                table: "ApplicationUser",
                column: "tenantId",
                unique: true,
                filter: "[tenantId] IS NOT NULL");
        }
    }
}
