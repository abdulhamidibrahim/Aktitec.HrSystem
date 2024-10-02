using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Emails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmailId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Cc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Bcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Archive = table.Column<bool>(type: "bit", nullable: false),
                    Starred = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false),
                    Trash = table.Column<bool>(type: "bit", nullable: false),
                    Selected = table.Column<bool>(type: "bit", nullable: false),
                    Spam = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_ApplicationUser_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Emails_ApplicationUser_SenderId",
                        column: x => x.SenderId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Emails_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MailAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_AttachmentId",
                table: "ModifiedRecord",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_EmailId",
                table: "ModifiedRecord",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_EmailId",
                table: "MailAttachment",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_TenantId",
                table: "MailAttachment",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ReceiverId",
                table: "Emails",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_SenderId",
                table: "Emails",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_TenantId",
                table: "Emails",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_Attachment_AttachmentId",
                table: "ModifiedRecord",
                column: "AttachmentId",
                principalTable: "MailAttachment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_Emails_EmailId",
                table: "ModifiedRecord",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_Attachment_AttachmentId",
                table: "ModifiedRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_Emails_EmailId",
                table: "ModifiedRecord");

            migrationBuilder.DropTable(
                name: "MailAttachment");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_AttachmentId",
                table: "ModifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_EmailId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "ModifiedRecord");
        }
    }
}
