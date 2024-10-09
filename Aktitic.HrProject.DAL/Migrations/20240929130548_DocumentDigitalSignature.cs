using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DocumentDigitalSignature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DigitalSignature",
                table: "Revisors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateKey",
                schema: "employee",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicKey",
                schema: "employee",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Revision",
                table: "Documents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FilesHash",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "First",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Last",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Next",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Previous",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileHash",
                table: "DocumentFiles",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DigitalSignature",
                table: "Revisors");

            migrationBuilder.DropColumn(
                name: "PrivateKey",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PublicKey",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "FilesHash",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "First",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Last",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Next",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Previous",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FileHash",
                table: "DocumentFiles");

            migrationBuilder.AlterColumn<string>(
                name: "Revision",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
