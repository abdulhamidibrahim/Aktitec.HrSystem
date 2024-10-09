using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyModules_AppModules_AppModulesId",
                table: "CompanyModules");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_CompanyRoles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions");

            migrationBuilder.RenameColumn(
                name: "AppModulesId",
                table: "CompanyModules",
                newName: "AppModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyModules_AppModulesId",
                table: "CompanyModules",
                newName: "IX_CompanyModules_AppModuleId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyModuleId",
                table: "RolePermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyRoleId",
                table: "RolePermissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_CompanyModuleId",
                table: "RolePermissions",
                column: "CompanyModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_CompanyRoleId",
                table: "RolePermissions",
                column: "CompanyRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyModules_AppModules_AppModuleId",
                table: "CompanyModules",
                column: "AppModuleId",
                principalTable: "AppModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_CompanyModules_CompanyModuleId",
                table: "RolePermissions",
                column: "CompanyModuleId",
                principalTable: "CompanyModules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_CompanyRoles_CompanyRoleId",
                table: "RolePermissions",
                column: "CompanyRoleId",
                principalTable: "CompanyRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyModules_AppModules_AppModuleId",
                table: "CompanyModules");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_CompanyModules_CompanyModuleId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_CompanyRoles_CompanyRoleId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_CompanyModuleId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_CompanyRoleId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "CompanyModuleId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "CompanyRoleId",
                table: "RolePermissions");

            migrationBuilder.RenameColumn(
                name: "AppModuleId",
                table: "CompanyModules",
                newName: "AppModulesId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyModules_AppModuleId",
                table: "CompanyModules",
                newName: "IX_CompanyModules_AppModulesId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyModules_AppModules_AppModulesId",
                table: "CompanyModules",
                column: "AppModulesId",
                principalTable: "AppModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_CompanyRoles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "CompanyRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
