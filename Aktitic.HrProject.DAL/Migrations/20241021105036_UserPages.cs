using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyInformation_ApplicationUser_UserId",
                table: "FamilyInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyInformation_Companies_TenantId",
                table: "FamilyInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_FamilyInformation_FamilyInformationId",
                table: "ModifiedRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilyInformation",
                table: "FamilyInformation");

            migrationBuilder.RenameTable(
                name: "FamilyInformation",
                newName: "FamilyInformations");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyInformation_UserId",
                table: "FamilyInformations",
                newName: "IX_FamilyInformations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyInformation_TenantId",
                table: "FamilyInformations",
                newName: "IX_FamilyInformations_TenantId");

            migrationBuilder.AddColumn<int>(
                name: "EducationInformationId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmergencyContactId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileExperienceId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChildrenNumber",
                table: "ApplicationUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmploymentSpouse",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "ApplicationUser",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatritalStatus",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PassportExpDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PinCode",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportsToId",
                table: "ApplicationUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilyInformations",
                table: "FamilyInformations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EducationInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_EducationInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationInformations_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationInformations_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneTwo = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_EmergencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProfileExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobPosition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodTo = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_ProfileExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileExperiences_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileExperiences_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_EducationInformationId",
                table: "ModifiedRecord",
                column: "EducationInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_EmergencyContactId",
                table: "ModifiedRecord",
                column: "EmergencyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ProfileExperienceId",
                table: "ModifiedRecord",
                column: "ProfileExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_ReportsToId",
                table: "ApplicationUser",
                column: "ReportsToId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationInformations_TenantId",
                table: "EducationInformations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationInformations_UserId",
                table: "EducationInformations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_TenantId",
                table: "EmergencyContacts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_UserId",
                table: "EmergencyContacts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileExperiences_TenantId",
                table: "ProfileExperiences",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileExperiences_UserId",
                table: "ProfileExperiences",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_ApplicationUser_ReportsToId",
                table: "ApplicationUser",
                column: "ReportsToId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyInformations_ApplicationUser_UserId",
                table: "FamilyInformations",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyInformations_Companies_TenantId",
                table: "FamilyInformations",
                column: "TenantId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_EducationInformations_EducationInformationId",
                table: "ModifiedRecord",
                column: "EducationInformationId",
                principalTable: "EducationInformations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_EmergencyContacts_EmergencyContactId",
                table: "ModifiedRecord",
                column: "EmergencyContactId",
                principalTable: "EmergencyContacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_FamilyInformations_FamilyInformationId",
                table: "ModifiedRecord",
                column: "FamilyInformationId",
                principalTable: "FamilyInformations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_ProfileExperiences_ProfileExperienceId",
                table: "ModifiedRecord",
                column: "ProfileExperienceId",
                principalTable: "ProfileExperiences",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_ApplicationUser_ReportsToId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyInformations_ApplicationUser_UserId",
                table: "FamilyInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyInformations_Companies_TenantId",
                table: "FamilyInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_EducationInformations_EducationInformationId",
                table: "ModifiedRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_EmergencyContacts_EmergencyContactId",
                table: "ModifiedRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_FamilyInformations_FamilyInformationId",
                table: "ModifiedRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_ProfileExperiences_ProfileExperienceId",
                table: "ModifiedRecord");

            migrationBuilder.DropTable(
                name: "EducationInformations");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropTable(
                name: "ProfileExperiences");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_EducationInformationId",
                table: "ModifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_EmergencyContactId",
                table: "ModifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_ProfileExperienceId",
                table: "ModifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_ReportsToId",
                table: "ApplicationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilyInformations",
                table: "FamilyInformations");

            migrationBuilder.DropColumn(
                name: "EducationInformationId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "EmergencyContactId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "ProfileExperienceId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ChildrenNumber",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "EmploymentSpouse",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "MatritalStatus",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "PassportExpDate",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "PinCode",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ReportsToId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "ApplicationUser");

            migrationBuilder.RenameTable(
                name: "FamilyInformations",
                newName: "FamilyInformation");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyInformations_UserId",
                table: "FamilyInformation",
                newName: "IX_FamilyInformation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyInformations_TenantId",
                table: "FamilyInformation",
                newName: "IX_FamilyInformation_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilyInformation",
                table: "FamilyInformation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyInformation_ApplicationUser_UserId",
                table: "FamilyInformation",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyInformation_Companies_TenantId",
                table: "FamilyInformation",
                column: "TenantId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_FamilyInformation_FamilyInformationId",
                table: "ModifiedRecord",
                column: "FamilyInformationId",
                principalTable: "FamilyInformation",
                principalColumn: "Id");
        }
    }
}
