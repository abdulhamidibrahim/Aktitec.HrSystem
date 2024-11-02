using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NotificationSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationId",
                schema: "employee",
                table: "Holiday",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveHolidayNotification = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_NotificationId",
                schema: "employee",
                table: "Holiday",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_CompanyId",
                table: "NotificationSettings",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holiday_NotificationSettings_NotificationId",
                schema: "employee",
                table: "Holiday",
                column: "NotificationId",
                principalTable: "NotificationSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holiday_NotificationSettings_NotificationId",
                schema: "employee",
                table: "Holiday");

            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.DropIndex(
                name: "IX_Holiday_NotificationId",
                schema: "employee",
                table: "Holiday");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "employee",
                table: "Holiday");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Companies");
        }
    }
}
