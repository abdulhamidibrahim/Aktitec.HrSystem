using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NotificationSettingsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActiveHolidayNotification",
                table: "NotificationSettings",
                newName: "Active");

            migrationBuilder.AddColumn<string>(
                name: "PageCode",
                table: "NotificationSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_PageCode",
                table: "NotificationSettings",
                column: "PageCode");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationSettings_AppPages_PageCode",
                table: "NotificationSettings",
                column: "PageCode",
                principalTable: "AppPages",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationSettings_AppPages_PageCode",
                table: "NotificationSettings");

            migrationBuilder.DropIndex(
                name: "IX_NotificationSettings_PageCode",
                table: "NotificationSettings");

            migrationBuilder.DropColumn(
                name: "PageCode",
                table: "NotificationSettings");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "NotificationSettings",
                newName: "ActiveHolidayNotification");
        }
    }
}
