using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RevisionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevisionDate",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "DocumentNo",
                table: "Documents",
                newName: "DocumentCode");

            migrationBuilder.AddColumn<int>(
                name: "RevisorId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueName",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Revisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revisors_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Revisors_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revisors_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_RevisorId",
                table: "ModifiedRecord",
                column: "RevisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisors_DocumentId",
                table: "Revisors",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisors_EmployeeId",
                table: "Revisors",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisors_TenantId",
                table: "Revisors",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_Revisors_RevisorId",
                table: "ModifiedRecord",
                column: "RevisorId",
                principalTable: "Revisors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_Revisors_RevisorId",
                table: "ModifiedRecord");

            migrationBuilder.DropTable(
                name: "Revisors");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_RevisorId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "RevisorId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "UniqueName",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "DocumentCode",
                table: "Documents",
                newName: "DocumentNo");

            migrationBuilder.AddColumn<DateTime>(
                name: "RevisionDate",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
