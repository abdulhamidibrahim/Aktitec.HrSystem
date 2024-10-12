using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedNewModulesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppModules",
                columns: new[] { "Id", "ArabicName", "CompanyModuleId", "Name" },
                values: new object[,]
                {
                    { 1, "الرئيسية", null, "Main" },
                    { 2, "الموظفون", null, "Employees" },
                    { 3, "الموارد البشرية", null, "HR" },
                    { 4, "الأداء", null, "Performance" },
                    { 5, "الإدارة", null, "Administration" }
                });

            migrationBuilder.InsertData(
                table: "AppSubModules",
                columns: new[] { "Id", "AppModuleId", "ArabicName", "Name" },
                values: new object[,]
                {
                    { 1, 1, "لوحة التحكم", "Dashboard" },
                    { 2, 1, "المستندات", "Documents" },
                    { 3, 1, "التطبيقات", "App" },
                    { 4, 2, "الموظفون", "Employees" },
                    { 5, 2, "العملاء", "Clients" },
                    { 6, 2, "المشاريع", "Projects" },
                    { 7, 2, "العملاء المحتملين", "Leads" },
                    { 8, 2, "التذاكر", "Tickets" },
                    { 9, 3, "المبيعات", "Sales" },
                    { 10, 3, "المحاسبة", "Accounting" },
                    { 11, 3, "الرواتب", "Payroll" },
                    { 12, 3, "السياسات", "Policies" },
                    { 13, 3, "التقارير", "Reports" },
                    { 14, 4, "الأداء", "Performance" },
                    { 15, 4, "الأهداف", "Goals" },
                    { 16, 4, "التدريب", "Training" },
                    { 17, 4, "الترقية", "Promotion" },
                    { 18, 4, "الاستقالة", "Resignation" },
                    { 19, 4, "فسخ العقد", "Termination" },
                    { 20, 5, "الأصول", "Assets" },
                    { 21, 5, "الإعدادات", "Settings" },
                    { 22, 5, "التعريب", "Localization" },
                    { 23, 5, "المستخدمون", "Users" }
                });

            migrationBuilder.InsertData(
                table: "AppPages",
                columns: new[] { "Code", "AppSubModuleId", "ArabicName", "Name" },
                values: new object[,]
                {
                    { "AdminDashboard", 1, "لوحة التحكم الإدارية", "Admin Dashboard" },
                    { "AllEmployees", 4, "كل الموظفين", "All Employees" },
                    { "AllUsers", 23, "كل المستخدمين", "All Users" },
                    { "Assets", 20, "الأصول", "Assets" },
                    { "AssetsCategories", 20, "فئات الأصول", "Assets Categories" },
                    { "AssetsManagement", 20, "إدارة الأصول", "Assets Management" },
                    { "AttendanceAdmin", 4, "الحضور (أدمن)", "Attendance (Admin)" },
                    { "AttendanceEmployee", 4, "الحضور (موظف)", "Attendance (Employee)" },
                    { "AttendanceReport", 13, "تقرير الحضور", "Attendance Report" },
                    { "Budgets", 10, "الميزانيات", "Budgets" },
                    { "BudgetsExpenses", 10, "نفقات الميزانيات", "Budgets Expenses" },
                    { "BudgetsRevenues", 10, "إيرادات الميزانيات", "Budgets Revenues" },
                    { "Calendar", 3, "تقويم", "Calendar" },
                    { "Categories", 10, "الفئات", "Categories" },
                    { "Chat", 3, "محادثة", "Chat" },
                    { "Clients", 5, "العملاء", "Clients" },
                    { "CompanySettings", 21, "إعدادات الشركة", "Company Settings" },
                    { "Contacts", 3, "جهات الاتصال", "Contacts" },
                    { "Contracts", 4, "العقود", "Contracts" },
                    { "CurrencySettings", 22, "إعدادات العملة", "Currency Settings" },
                    { "DailyReport", 13, "تقرير يومي", "Daily Report" },
                    { "Departments", 4, "الأقسام", "Departments" },
                    { "Designations", 4, "التسميات", "Designations" },
                    { "DocumentsDetailsView", 2, "عرض تفاصيل المستندات", "Documents Details View" },
                    { "DocumentsManager", 2, "مدير المستندات", "Documents Manager" },
                    { "DocumentsWorkflows", 2, "سير عمل المستندات", "Documents Workflows" },
                    { "Email", 3, "البريد الإلكتروني", "Email" },
                    { "EmailSettings", 21, "إعدادات البريد الإلكتروني", "Email Settings" },
                    { "EmployeeDashboard", 1, "لوحة تحكم الموظفين", "Employee Dashboard" },
                    { "EmployeeReport", 13, "تقرير الموظفين", "Employee Report" },
                    { "EmployeeSalary", 11, "رواتب الموظفين", "Employee Salary" },
                    { "Estimate", 9, "تقدير", "Estimate" },
                    { "ExpenseReport", 13, "تقرير النفقات", "Expense Report" },
                    { "Expenses", 9, "النفقات", "Expenses" },
                    { "GoalList", 15, "قائمة الأهداف", "Goal List" },
                    { "GoalType", 15, "نوع الهدف", "Goal Type" },
                    { "Holidays", 4, "العطل", "Holidays" },
                    { "HRSettings", 21, "إعدادات الموارد البشرية", "HR Settings" },
                    { "InvoiceReport", 13, "تقرير الفواتير", "Invoice Report" },
                    { "Invoices", 9, "الفواتير", "Invoices" },
                    { "LanguageSettings", 22, "إعدادات اللغة", "Language Settings" },
                    { "Leads", 7, "العملاء المحتملين", "Leads" },
                    { "LeaveReport", 13, "تقرير الإجازات", "Leave Report" },
                    { "LeavesAdmin", 4, "الإجازات (أدمن)", "Leaves (Admin)" },
                    { "LeavesEmployee", 4, "الإجازات (موظف)", "Leaves (Employee)" },
                    { "LeaveSettings", 4, "إعدادات الإجازات", "Leave Settings" },
                    { "NotificationSettings", 21, "إعدادات الإشعارات", "Notification Settings" },
                    { "Overtime", 4, "الوقت الإضافي", "Overtime" },
                    { "Payments", 9, "المدفوعات", "Payments" },
                    { "PaymentsReport", 13, "تقرير المدفوعات", "Payments Report" },
                    { "PayrollItems", 11, "عناصر الرواتب", "Payroll Items" },
                    { "PayslipReport", 13, "تقرير قسيمة الراتب", "Payslip Report" },
                    { "PerformanceAppraisal", 14, "تقييم الأداء", "Performance Appraisal" },
                    { "PerformanceIndicator", 14, "مؤشر الأداء", "Performance Indicator" },
                    { "PerformanceReview", 14, "مراجعة الأداء", "Performance Review" },
                    { "Policies", 12, "السياسات", "Policies" },
                    { "ProjectReport", 13, "تقرير المشروع", "Project Report" },
                    { "Projects", 6, "المشاريع", "Projects" },
                    { "Promotion", 17, "ترقية", "Promotion" },
                    { "ProvidentFund", 9, "صندوق الادخار", "Provident Fund" },
                    { "Resignation", 18, "استقالة", "Resignation" },
                    { "Role&Permissions", 21, "الأدوار والصلاحيات", "Role & Permissions" },
                    { "Shift&Schedule", 4, "جدول المناوبة", "Shift & Schedule" },
                    { "TaskBoard", 6, "لوحة المهام", "Task Board" },
                    { "TaskReport", 13, "تقرير المهام", "Task Report" },
                    { "Tasks", 6, "المهام", "Tasks" },
                    { "Taxes", 9, "الضرائب", "Taxes" },
                    { "Termination", 19, "فسخ العقد", "Termination" },
                    { "Tickets", 8, "التذاكر", "Tickets" },
                    { "Trainers", 16, "المدربون", "Trainers" },
                    { "TrainingList", 16, "قائمة التدريب", "Training List" },
                    { "TrainingType", 16, "نوع التدريب", "Training Type" },
                    { "UserActivityLog", 23, "سجل نشاط المستخدمين", "User Activity Log" },
                    { "UserReport", 13, "تقرير المستخدم", "User Report" },
                    { "UserRoles", 23, "أدوار المستخدمين", "User Roles" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: "AllUsers");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Assets");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "AssetsCategories");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "AssetsManagement");

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
                keyValue: "CompanySettings");

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
                keyValue: "CurrencySettings");

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
                keyValue: "EmailSettings");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "EmployeeDashboard");

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
                keyValue: "HRSettings");

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
                keyValue: "LanguageSettings");

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
                keyValue: "NotificationSettings");

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
                keyValue: "PerformanceIndicator");

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
                keyValue: "Role&Permissions");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "Shift&Schedule");

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
                keyValue: "UserActivityLog");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "UserReport");

            migrationBuilder.DeleteData(
                table: "AppPages",
                keyColumn: "Code",
                keyValue: "UserRoles");

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
        }
    }
}
