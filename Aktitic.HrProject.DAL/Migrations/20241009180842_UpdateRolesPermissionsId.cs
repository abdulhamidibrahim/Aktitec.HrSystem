using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolesPermissionsId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_ApplicationUser_UserId",
                schema: "project",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_CompanyRoles_CompanyRoleId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserId",
                schema: "project",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "RolePermissions");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyRoleId",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_CompanyRoles_CompanyRoleId",
                table: "RolePermissions",
                column: "CompanyRoleId",
                principalTable: "CompanyRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_CompanyRoles_CompanyRoleId",
                table: "RolePermissions");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyRoleId",
                table: "RolePermissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId",
                schema: "project",
                table: "Permissions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_ApplicationUser_UserId",
                schema: "project",
                table: "Permissions",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_CompanyRoles_CompanyRoleId",
                table: "RolePermissions",
                column: "CompanyRoleId",
                principalTable: "CompanyRoles",
                principalColumn: "Id");
        }
    }
}
