using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LogAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "AuditLogs");

            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "AuditLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LogActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogActions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ActionId",
                table: "AuditLogs",
                column: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_LogActions_ActionId",
                table: "AuditLogs",
                column: "ActionId",
                principalTable: "LogActions",
                principalColumn: "Id");
            
            migrationBuilder.InsertData(
                table: "LogActions", 
                columns: new[] { "ArabicName", "Name" },
                values: new object[,]
                {
                    { "إضافة", "Added" },
                    { "تعديل", "Modified" },
                    { "حذف", "Delete" },
                    { "تحميل", "Download" },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_LogActions_ActionId",
                table: "AuditLogs");

            migrationBuilder.DropTable(
                name: "LogActions");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_ActionId",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "AuditLogs");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
