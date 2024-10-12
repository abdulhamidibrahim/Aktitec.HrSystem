using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ModulesArabicNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AppSubModules",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AppModules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "AppSubModules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "AppModules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "AppSubModules");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "AppModules");

            migrationBuilder.InsertData(
                table: "AppModules",
                columns: new[] { "Id", "CompanyModuleId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Main" },
                    { 2, null, "Employees" },
                    { 3, null, "HR" },
                    { 4, null, "Performance" },
                    { 5, null, "Administration" },
                    { 6, null, "Pages" }
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
        }
    }
}
