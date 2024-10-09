using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AppModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PrintSize",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AppSubModuleId",
                table: "AppPages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AppPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AppModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSubModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppModuleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSubModules_AppModules_AppModuleId",
                        column: x => x.AppModuleId,
                        principalTable: "AppModules",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AppModules",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Main" },
                    { 2, "Employees" },
                    { 3, "HR" },
                    { 4, "Performance" },
                    { 5, "Administration" },
                    { 6, "Pages" }
                });

            migrationBuilder.InsertData(
                table: "AppSubModules",
                columns: new[] { "Id", "AppModuleId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Dashboard" },
                    { 2, 1, "App" },
                    { 3, 2, "Employees" },
                    { 4, 2, "Clients" },
                    { 5, 2, "Projects" },
                    { 6, 2, "Leads" },
                    { 7, 2, "Tickets" },
                    { 8, 3, "Sales" },
                    { 9, 3, "Accounting" },
                    { 10, 3, "Payroll" },
                    { 11, 3, "Policies" },
                    { 12, 3, "Reports" },
                    { 13, 4, "Performance" },
                    { 14, 4, "Goals" },
                    { 15, 4, "Training" },
                    { 16, 4, "Promotion" },
                    { 17, 4, "Resignation" },
                    { 18, 4, "Termination" },
                    { 19, 5, "Assets" },
                    { 20, 5, "Jobs" },
                    { 21, 5, "Knowledgebase" },
                    { 22, 5, "Activities" },
                    { 23, 5, "Users" },
                    { 24, 5, "Companies" },
                    { 25, 5, "Licenses" },
                    { 26, 5, "Notifications" },
                    { 27, 5, "User Activities" },
                    { 28, 5, "Trash" },
                    { 29, 5, "Settings" },
                    { 30, 6, "Profile" }
                });

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
                name: "IX_AppPages_AppSubModuleId",
                table: "AppPages",
                column: "AppSubModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSubModules_AppModuleId",
                table: "AppSubModules",
                column: "AppModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPages_AppSubModules_AppSubModuleId",
                table: "AppPages",
                column: "AppSubModuleId",
                principalTable: "AppSubModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPages_AppSubModules_AppSubModuleId",
                table: "AppPages");

            migrationBuilder.DropTable(
                name: "AppSubModules");

            migrationBuilder.DropTable(
                name: "AppModules");

            migrationBuilder.DropIndex(
                name: "IX_AppPages_AppSubModuleId",
                table: "AppPages");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DropColumn(
                name: "AppSubModuleId",
                table: "AppPages");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "AppPages");

            migrationBuilder.AlterColumn<string>(
                name: "PrintSize",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
