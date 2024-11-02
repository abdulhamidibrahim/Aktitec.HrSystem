using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "PhoneTwo",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "Relationship",
                table: "EmergencyContacts");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PeriodTo",
                table: "ProfileExperiences",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PeriodFrom",
                table: "ProfileExperiences",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "ProfileExperiences",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "JobPosition",
                table: "ProfileExperiences",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "ProfileExperiences",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DoB",
                table: "FamilyInformations",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryName",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhone",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhoneTwo",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryRelationship",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryName",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryPhone",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryPhoneTwo",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryRelationship",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "EducationInformations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartingDate",
                table: "EducationInformations",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Institution",
                table: "EducationInformations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "EducationInformations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "EducationInformations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CompleteDate",
                table: "EducationInformations",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "project",
                table: "Client",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ApplicationUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                schema: "project",
                table: "Client",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_ClientId",
                table: "ApplicationUser",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Client_ClientId",
                table: "ApplicationUser",
                column: "ClientId",
                principalSchema: "project",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_ApplicationUser_UserId",
                schema: "project",
                table: "Client",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Client_ClientId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_ApplicationUser_UserId",
                schema: "project",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_UserId",
                schema: "project",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_ClientId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "PrimaryName",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "PrimaryPhone",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "PrimaryPhoneTwo",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "PrimaryRelationship",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "SecondaryName",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "SecondaryPhone",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "SecondaryPhoneTwo",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "SecondaryRelationship",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "project",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodTo",
                table: "ProfileExperiences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodFrom",
                table: "ProfileExperiences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "ProfileExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobPosition",
                table: "ProfileExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "ProfileExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DoB",
                table: "FamilyInformations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneTwo",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Relationship",
                table: "EmergencyContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "EducationInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartingDate",
                table: "EducationInformations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Institution",
                table: "EducationInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "EducationInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "EducationInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompleteDate",
                table: "EducationInformations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
