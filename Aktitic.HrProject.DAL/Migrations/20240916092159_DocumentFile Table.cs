using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DocumentFileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUsers_File_FileId",
                table: "FileUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_File_FileId",
                table: "ModifiedRecord");

            migrationBuilder.DropTable(
                name: "File",
                schema: "employee");

            migrationBuilder.DropIndex(
                name: "IX_FileUsers_FileId",
                table: "FileUsers");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "ModifiedRecord",
                newName: "DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_ModifiedRecord_FileId",
                table: "ModifiedRecord",
                newName: "IX_ModifiedRecord_DocumentId");

            migrationBuilder.AddColumn<int>(
                name: "DocumentFileId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "FileUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Confidential = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    ExpensesId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalSchema: "client",
                        principalTable: "Expenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "project",
                        principalTable: "Ticket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    FileNumber = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_DocumentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentFiles_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentFiles_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_DocumentFileId",
                table: "ModifiedRecord",
                column: "DocumentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileUsers_DocumentId",
                table: "FileUsers",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_DocumentId",
                table: "DocumentFiles",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_TenantId",
                table: "DocumentFiles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ExpensesId",
                table: "Documents",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProjectId",
                table: "Documents",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TenantId",
                table: "Documents",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TicketId",
                table: "Documents",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUsers_Documents_DocumentId",
                table: "FileUsers",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_DocumentFiles_DocumentFileId",
                table: "ModifiedRecord",
                column: "DocumentFileId",
                principalTable: "DocumentFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_Documents_DocumentId",
                table: "ModifiedRecord",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUsers_Documents_DocumentId",
                table: "FileUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_DocumentFiles_DocumentFileId",
                table: "ModifiedRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_Documents_DocumentId",
                table: "ModifiedRecord");

            migrationBuilder.DropTable(
                name: "DocumentFiles");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_DocumentFileId",
                table: "ModifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_FileUsers_DocumentId",
                table: "FileUsers");

            migrationBuilder.DropColumn(
                name: "DocumentFileId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "FileUsers");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "ModifiedRecord",
                newName: "DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_ModifiedRecord_DocumentId",
                table: "ModifiedRecord",
                newName: "IX_ModifiedRecord_FileId");

            migrationBuilder.CreateTable(
                name: "File",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpensesId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Confidential = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PrintSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_File_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_File_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalSchema: "client",
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_File_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_File_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "project",
                        principalTable: "Ticket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUsers_FileId",
                table: "FileUsers",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_File_ExpensesId",
                schema: "employee",
                table: "File",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_File_ProjectId",
                schema: "employee",
                table: "File",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_File_TenantId",
                schema: "employee",
                table: "File",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_File_TicketId",
                schema: "employee",
                table: "File",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_File_UserId",
                schema: "employee",
                table: "File",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUsers_File_FileId",
                table: "FileUsers",
                column: "DocumentId",
                principalSchema: "employee",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_File_FileId",
                table: "ModifiedRecord",
                column: "DocumentId",
                principalSchema: "employee",
                principalTable: "File",
                principalColumn: "Id");
        }
    }
}
