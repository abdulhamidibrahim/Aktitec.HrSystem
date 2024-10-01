using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFileManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "FileUsers",
                newName: "Confidential");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "employee",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Confidential",
                schema: "employee",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PrintSize",
                schema: "employee",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Revision",
                schema: "employee",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RevisionDate",
                schema: "employee",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "employee",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "employee",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confidential",
                schema: "employee",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "PrintSize",
                schema: "employee",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Revision",
                schema: "employee",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "RevisionDate",
                schema: "employee",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "employee",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "employee",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Confidential",
                table: "FileUsers",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                schema: "employee",
                table: "Documents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
