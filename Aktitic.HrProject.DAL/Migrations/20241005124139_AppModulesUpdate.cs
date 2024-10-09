using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AppModulesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_AppPages_AppPagesId",
                table: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_AppPagesId",
                table: "AuditLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppPages",
                table: "AppPages");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 85);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppPages");

            migrationBuilder.AddColumn<int>(
                name: "CompanyModuleId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyRoleId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RolePermissionsId",
                table: "ModifiedRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppPagesCode",
                table: "AuditLogs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AppPages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CompanyModuleId",
                table: "AppModules",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppPages",
                table: "AppPages",
                column: "Code");

            migrationBuilder.CreateTable(
                name: "CompanyModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    AppModulesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CompanyModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyModules_AppModules_AppModulesId",
                        column: x => x.AppModulesId,
                        principalTable: "AppModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyModules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyModules_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CompanyRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyRoles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyRoles_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Edit = table.Column<bool>(type: "bit", nullable: false),
                    Add = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    Import = table.Column<bool>(type: "bit", nullable: false),
                    Export = table.Column<bool>(type: "bit", nullable: false),
                    PageCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_AppPages_PageCode",
                        column: x => x.PageCode,
                        principalTable: "AppPages",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_CompanyRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CompanyRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 1,
                column: "CompanyModuleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 2,
                column: "CompanyModuleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 3,
                column: "CompanyModuleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 4,
                column: "CompanyModuleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 5,
                column: "CompanyModuleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 6,
                column: "CompanyModuleId",
                value: null);

            migrationBuilder.InsertData(
                table: "AppPages",
                columns: new[] { "Code", "AppSubModuleId", "ArabicName", "Name" },
                values: new object[,]
                {
                    { "Activities", 22, "الأنشطة", "Activities" },
                    { "AdminDashboard", 1, "لوحة التحكم الإدارية", "Admin Dashboard" },
                    { "AllEmployees", 3, "كل الموظفين", "All Employees" },
                    { "AppliedCandidates", 20, "المرشحون المتقدمون", "Applied Candidates" },
                    { "AptitudeResults", 20, "نتائج الكفاءة", "Aptitude Results" },
                    { "Assets", 19, "الأصول", "Assets" },
                    { "AttendanceAdmin", 3, "الحضور (أدمن)", "Attendance (Admin)" },
                    { "AttendanceEmployee", 3, "الحضور (موظف)", "Attendance (Employee)" },
                    { "AttendanceReport", 12, "تقرير الحضور", "Attendance Report" },
                    { "Budgets", 9, "الميزانيات", "Budgets" },
                    { "BudgetsExpenses", 9, "نفقات الميزانيات", "Budgets Expenses" },
                    { "BudgetsRevenues", 9, "إيرادات الميزانيات", "Budgets Revenues" },
                    { "Calendar", 2, "تقويم", "Calendar" },
                    { "CandidatesList", 20, "قائمة المرشحين", "Candidates List" },
                    { "Categories", 9, "الفئات", "Categories" },
                    { "Chat", 2, "محادثة", "Chat" },
                    { "Clients", 4, "العملاء", "Clients" },
                    { "Companies", 24, "الشركات", "Companies" },
                    { "Contacts", 2, "جهات الاتصال", "Contacts" },
                    { "Contracts", 3, "عقود", "Contracts" },
                    { "DailyReport", 12, "تقرير يومي", "Daily Report" },
                    { "Departments", 3, "الأقسام", "Departments" },
                    { "Designations", 3, "التسميات", "Designations" },
                    { "DocumentsDetailsView", 2, "عرض تفاصيل الملفات", "Documents Details View" },
                    { "DocumentsManager", 2, "مدير الملفات", "Documents Manager" },
                    { "DocumentsWorkflows", 2, "سير عمل الملفات", "Documents Workflows" },
                    { "Email", 2, "البريد الإلكتروني", "Email" },
                    { "EmployeeDashboard", 1, "لوحة تحكم الموظفين", "Employee Dashboard" },
                    { "EmployeeProfile", 30, "ملف الموظف", "Employee Profile" },
                    { "EmployeeReport", 12, "تقرير الموظفين", "Employee Report" },
                    { "EmployeeSalary", 10, "رواتب الموظفين", "Employee Salary" },
                    { "Estimate", 8, "تقدير", "Estimate" },
                    { "ExpenseReport", 12, "تقرير النفقات", "Expense Report" },
                    { "Expenses", 8, "النفقات", "Expenses" },
                    { "ExperienceLevel", 20, "مستوى الخبرة", "Experience Level" },
                    { "GoalList", 14, "قائمة الأهداف", "Goal List" },
                    { "GoalType", 14, "نوع الهدف", "Goal Type" },
                    { "Holidays", 3, "العطل", "Holidays" },
                    { "InterviewQuestions", 20, "أسئلة المقابلة", "Interview Questions" },
                    { "InvoiceReport", 12, "تقرير الفواتير", "Invoice Report" },
                    { "Invoices", 8, "الفواتير", "Invoices" },
                    { "JobsDashboard", 20, "لوحة تحكم الوظائف", "Jobs Dashboard" },
                    { "Knowledgebase", 21, "قاعدة المعرفة", "Knowledgebase" },
                    { "Leads", 6, "العملاء المحتملين", "Leads" },
                    { "LeaveReport", 12, "تقرير الإجازات", "Leave Report" },
                    { "LeavesAdmin", 3, "الإجازات (أدمن)", "Leaves (Admin)" },
                    { "LeavesEmployee", 3, "الإجازات (موظف)", "Leaves (Employee)" },
                    { "LeaveSettings", 3, "إعدادات الإجازات", "Leave Settings" },
                    { "Licenses", 25, "التراخيص", "Licenses" },
                    { "ManageJobs", 20, "إدارة الوظائف", "Manage Jobs" },
                    { "ManageResumes", 20, "إدارة السير الذاتية", "Manage Resumes" },
                    { "Notifications", 26, "الإشعارات", "Notifications" },
                    { "OfferApprovals", 20, "الموافقات على العروض", "Offer Approvals" },
                    { "Overtime", 3, "الوقت الإضافي", "Overtime" },
                    { "Payments", 8, "المدفوعات", "Payments" },
                    { "PaymentsReport", 12, "تقرير المدفوعات", "Payments Report" },
                    { "PayrollItems", 10, "عناصر الرواتب", "Payroll Items" },
                    { "PayslipReport", 12, "تقرير قسيمة الراتب", "Payslip Report" },
                    { "PerformanceAppraisal", 13, "تقييم الأداء", "Performance Appraisal" },
                    { "Performanceindicator", 13, "مؤشر الأداء", "Performance indicator" },
                    { "PerformanceReview", 13, "مراجعة الأداء", "Performance Review" },
                    { "Policies", 11, "السياسات", "Policies" },
                    { "ProjectReport", 12, "تقرير المشروع", "Project Report" },
                    { "Projects", 5, "المشاريع", "Projects" },
                    { "Promotion", 16, "ترقية", "Promotion" },
                    { "ProvidentFund", 8, "صندوق الادخار", "Provident Fund" },
                    { "Resignation", 17, "استقالة", "Resignation" },
                    { "ScheduleTiming", 20, "توقيت الجدول الزمني", "Schedule Timing" },
                    { "Settings", 29, "الإعدادات", "Settings" },
                    { "Shift&Schedule", 3, "جدول المناوبة", "Shift & Schedule" },
                    { "ShortlistCandidates", 20, "قائمة المرشحين المختصرة", "Shortlist Candidates" },
                    { "TaskBoard", 5, "لوحة المهام", "Task Board" },
                    { "TaskReport", 12, "تقرير المهام", "Task Report" },
                    { "Tasks", 5, "المهام", "Tasks" },
                    { "Taxes", 8, "الضرائب", "Taxes" },
                    { "Termination", 18, "فسخ العقد", "Termination" },
                    { "Tickets", 7, "التذاكر", "Tickets" },
                    { "Trainers", 15, "المدربون", "Trainers" },
                    { "TrainingList", 15, "قائمة التدريب", "Training List" },
                    { "TrainingType", 15, "نوع التدريب", "Training Type" },
                    { "Trash", 28, "المهملات", "Trash" },
                    { "UserActivities", 27, "أنشطة المستخدم", "User Activities" },
                    { "UserDashboard", 20, "لوحة تحكم المستخدم", "User Dashboard" },
                    { "UserReport", 12, "تقرير المستخدم", "User Report" },
                    { "Users", 23, "المستخدمون", "Users" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_CompanyModuleId",
                table: "ModifiedRecord",
                column: "CompanyModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_CompanyRoleId",
                table: "ModifiedRecord",
                column: "CompanyRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_RolePermissionsId",
                table: "ModifiedRecord",
                column: "RolePermissionsId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_AppPagesCode",
                table: "AuditLogs",
                column: "AppPagesCode");

            migrationBuilder.CreateIndex(
                name: "IX_AppModules_CompanyModuleId",
                table: "AppModules",
                column: "CompanyModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyModules_AppModulesId",
                table: "CompanyModules",
                column: "AppModulesId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyModules_CompanyId",
                table: "CompanyModules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyModules_TenantId",
                table: "CompanyModules",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRoles_CompanyId",
                table: "CompanyRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRoles_TenantId",
                table: "CompanyRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PageCode",
                table: "RolePermissions",
                column: "PageCode");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_TenantId",
                table: "RolePermissions",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppModules_CompanyModules_CompanyModuleId",
                table: "AppModules",
                column: "CompanyModuleId",
                principalTable: "CompanyModules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_AppPages_AppPagesCode",
                table: "AuditLogs",
                column: "AppPagesCode",
                principalTable: "AppPages",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_CompanyModules_CompanyModuleId",
                table: "ModifiedRecord",
                column: "CompanyModuleId",
                principalTable: "CompanyModules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_CompanyRoles_CompanyRoleId",
                table: "ModifiedRecord",
                column: "CompanyRoleId",
                principalTable: "CompanyRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRecord_RolePermissions_RolePermissionsId",
                table: "ModifiedRecord",
                column: "RolePermissionsId",
                principalTable: "RolePermissions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppModules_CompanyModules_CompanyModuleId",
                table: "AppModules");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_AppPages_AppPagesCode",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_CompanyModules_CompanyModuleId",
                table: "ModifiedRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_CompanyRoles_CompanyRoleId",
                table: "ModifiedRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedRecord_RolePermissions_RolePermissionsId",
                table: "ModifiedRecord");

            migrationBuilder.DropTable(
                name: "CompanyModules");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "CompanyRoles");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_CompanyModuleId",
                table: "ModifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_CompanyRoleId",
                table: "ModifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedRecord_RolePermissionsId",
                table: "ModifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_AppPagesCode",
                table: "AuditLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppPages",
                table: "AppPages");

            migrationBuilder.DropIndex(
                name: "IX_AppModules_CompanyModuleId",
                table: "AppModules");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Activities");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "AdminDashboard");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "AllEmployees");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "AppliedCandidates");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "AptitudeResults");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Assets");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "AttendanceAdmin");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "AttendanceEmployee");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "AttendanceReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Budgets");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "BudgetsExpenses");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "BudgetsRevenues");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Calendar");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "CandidatesList");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Categories");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Chat");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Clients");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Companies");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Contacts");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Contracts");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "DailyReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Departments");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Designations");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "DocumentsDetailsView");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "DocumentsManager");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "DocumentsWorkflows");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Email");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "EmployeeDashboard");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "EmployeeProfile");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "EmployeeReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "EmployeeSalary");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Estimate");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "ExpenseReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Expenses");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "ExperienceLevel");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "GoalList");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "GoalType");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Holidays");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "InterviewQuestions");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "InvoiceReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Invoices");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "JobsDashboard");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Knowledgebase");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Leads");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "LeaveReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "LeavesAdmin");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "LeavesEmployee");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "LeaveSettings");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Licenses");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "ManageJobs");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "ManageResumes");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Notifications");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "OfferApprovals");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Overtime");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Payments");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "PaymentsReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "PayrollItems");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "PayslipReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "PerformanceAppraisal");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Performanceindicator");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "PerformanceReview");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Policies");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "ProjectReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Projects");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Promotion");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "ProvidentFund");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Resignation");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "ScheduleTiming");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Settings");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Shift&Schedule");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "ShortlistCandidates");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "TaskBoard");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "TaskReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Tasks");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Taxes");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Termination");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Tickets");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Trainers");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "TrainingList");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "TrainingType");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Trash");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "UserActivities");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "UserDashboard");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "UserReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyModuleId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "CompanyRoleId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "RolePermissionsId",
                table: "ModifiedRecord");

            migrationBuilder.DropColumn(
                name: "AppPagesCode",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "CompanyModuleId",
                table: "AppModules");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AppPages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppPages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppPages",
                table: "AppPages",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AppPages",
                columns: new[] { "Id", "AppSubModuleId", "ArabicName", "Code", "Name" },
                values: new object[,]
                {
                    { 1, 1, "لوحة التحكم الإدارية", "AdminDashboard", "Admin Dashboard" },
                    { 2, 1, "لوحة تحكم الموظفين", "EmployeeDashboard", "Employee Dashboard" },
                    { 3, 2, "محادثة", "Chat", "Chat" },
                    { 4, 2, "تقويم", "Calendar", "Calendar" },
                    { 5, 2, "جهات الاتصال", "Contacts", "Contacts" },
                    { 6, 2, "البريد الإلكتروني", "Email", "Email" },
                    { 7, 2, "مدير الملفات", "DocumentsManager", "Documents Manager" },
                    { 8, 2, "سير عمل الملفات", "DocumentsWorkflows", "Documents Workflows" },
                    { 9, 2, "عرض تفاصيل الملفات", "DocumentsDetailsView", "Documents Details View" },
                    { 10, 3, "كل الموظفين", "AllEmployees", "All Employees" },
                    { 11, 3, "عقود", "Contracts", "Contracts" },
                    { 12, 3, "العطل", "Holidays", "Holidays" },
                    { 13, 3, "الإجازات (أدمن)", "LeavesAdmin", "Leaves (Admin)" },
                    { 14, 3, "الإجازات (موظف)", "LeavesEmployee", "Leaves (Employee)" },
                    { 15, 3, "إعدادات الإجازات", "LeaveSettings", "Leave Settings" },
                    { 16, 3, "الحضور (أدمن)", "AttendanceAdmin", "Attendance (Admin)" },
                    { 17, 3, "الحضور (موظف)", "AttendanceEmployee", "Attendance (Employee)" },
                    { 18, 3, "الأقسام", "Departments", "Departments" },
                    { 19, 3, "التسميات", "Designations", "Designations" },
                    { 20, 3, "جدول المناوبة", "Shift&Schedule", "Shift & Schedule" },
                    { 21, 3, "الوقت الإضافي", "Overtime", "Overtime" },
                    { 22, 4, "العملاء", "Clients", "Clients" },
                    { 23, 5, "المشاريع", "Projects", "Projects" },
                    { 24, 5, "المهام", "Tasks", "Tasks" },
                    { 25, 5, "لوحة المهام", "TaskBoard", "Task Board" },
                    { 26, 6, "العملاء المحتملين", "Leads", "Leads" },
                    { 27, 7, "التذاكر", "Tickets", "Tickets" },
                    { 28, 8, "تقدير", "Estimate", "Estimate" },
                    { 29, 8, "الفواتير", "Invoices", "Invoices" },
                    { 30, 8, "المدفوعات", "Payments", "Payments" },
                    { 31, 8, "النفقات", "Expenses", "Expenses" },
                    { 32, 8, "صندوق الادخار", "ProvidentFund", "Provident Fund" },
                    { 33, 8, "الضرائب", "Taxes", "Taxes" },
                    { 34, 9, "الفئات", "Categories", "Categories" },
                    { 35, 9, "الميزانيات", "Budgets", "Budgets" },
                    { 36, 9, "نفقات الميزانيات", "BudgetsExpenses", "Budgets Expenses" },
                    { 37, 9, "إيرادات الميزانيات", "BudgetsRevenues", "Budgets Revenues" },
                    { 38, 10, "رواتب الموظفين", "EmployeeSalary", "Employee Salary" },
                    { 39, 10, "عناصر الرواتب", "PayrollItems", "Payroll Items" },
                    { 40, 11, "السياسات", "Policies", "Policies" },
                    { 41, 12, "تقرير النفقات", "ExpenseReport", "Expense Report" },
                    { 42, 12, "تقرير الفواتير", "InvoiceReport", "Invoice Report" },
                    { 43, 12, "تقرير المدفوعات", "PaymentsReport", "Payments Report" },
                    { 44, 12, "تقرير المشروع", "ProjectReport", "Project Report" },
                    { 45, 12, "تقرير المهام", "TaskReport", "Task Report" },
                    { 46, 12, "تقرير المستخدم", "UserReport", "User Report" },
                    { 47, 12, "تقرير الموظفين", "EmployeeReport", "Employee Report" },
                    { 48, 12, "تقرير قسيمة الراتب", "PayslipReport", "Payslip Report" },
                    { 49, 12, "تقرير الحضور", "AttendanceReport", "Attendance Report" },
                    { 50, 12, "تقرير الإجازات", "LeaveReport", "Leave Report" },
                    { 51, 12, "تقرير يومي", "DailyReport", "Daily Report" },
                    { 52, 13, "مؤشر الأداء", "Performanceindicator", "Performance indicator" },
                    { 53, 13, "مراجعة الأداء", "PerformanceReview", "Performance Review" },
                    { 54, 13, "تقييم الأداء", "PerformanceAppraisal", "Performance Appraisal" },
                    { 55, 14, "قائمة الأهداف", "GoalList", "Goal List" },
                    { 56, 14, "نوع الهدف", "GoalType", "Goal Type" },
                    { 57, 15, "قائمة التدريب", "TrainingList", "Training List" },
                    { 58, 15, "المدربون", "Trainers", "Trainers" },
                    { 59, 15, "نوع التدريب", "TrainingType", "Training Type" },
                    { 60, 16, "ترقية", "Promotion", "Promotion" },
                    { 61, 17, "استقالة", "Resignation", "Resignation" },
                    { 62, 18, "فسخ العقد", "Termination", "Termination" },
                    { 63, 19, "الأصول", "Assets", "Assets" },
                    { 64, 20, "لوحة تحكم المستخدم", "UserDashboard", "User Dashboard" },
                    { 65, 20, "لوحة تحكم الوظائف", "JobsDashboard", "Jobs Dashboard" },
                    { 66, 20, "إدارة الوظائف", "ManageJobs", "Manage Jobs" },
                    { 67, 20, "إدارة السير الذاتية", "ManageResumes", "Manage Resumes" },
                    { 68, 20, "قائمة المرشحين المختصرة", "ShortlistCandidates", "Shortlist Candidates" },
                    { 69, 20, "أسئلة المقابلة", "InterviewQuestions", "Interview Questions" },
                    { 70, 20, "الموافقات على العروض", "OfferApprovals", "Offer Approvals" },
                    { 71, 20, "مستوى الخبرة", "ExperienceLevel", "Experience Level" },
                    { 72, 20, "قائمة المرشحين", "CandidatesList", "Candidates List" },
                    { 73, 20, "توقيت الجدول الزمني", "ScheduleTiming", "Schedule Timing" },
                    { 74, 20, "نتائج الكفاءة", "AptitudeResults", "Aptitude Results" },
                    { 75, 20, "المرشحون المتقدمون", "AppliedCandidates", "Applied Candidates" },
                    { 76, 21, "قاعدة المعرفة", "Knowledgebase", "Knowledgebase" },
                    { 77, 22, "الأنشطة", "Activities", "Activities" },
                    { 78, 23, "المستخدمون", "Users", "Users" },
                    { 79, 24, "الشركات", "Companies", "Companies" },
                    { 80, 25, "التراخيص", "Licenses", "Licenses" },
                    { 81, 26, "الإشعارات", "Notifications", "Notifications" },
                    { 82, 27, "أنشطة المستخدم", "UserActivities", "User Activities" },
                    { 83, 28, "المهملات", "Trash", "Trash" },
                    { 84, 29, "الإعدادات", "Settings", "Settings" },
                    { 85, 30, "ملف الموظف", "EmployeeProfile", "Employee Profile" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_AppPagesId",
                table: "AuditLogs",
                column: "AppPagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_AppPages_AppPagesId",
                table: "AuditLogs",
                column: "AppPagesId",
                principalTable: "AppPages",
                principalColumn: "Id");
        }
    }
}
