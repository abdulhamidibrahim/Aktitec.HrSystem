using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AppPagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageName",
                table: "AuditLogs");

            migrationBuilder.AddColumn<int>(
                name: "AppPagesId",
                table: "AuditLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPages", x => x.Id);
                });

            migrationBuilder.InsertData(
    table: "AppPages", 
    columns: new[] { "ArabicName", "Name" },
    values: new object[,]
    {
            { "محادثة", "Chat" },
            { "تقويم", "Calendar" },
            { "جهات الاتصال", "Contacts" },
            { "مدير الملفات", "Documents Manager" },
            { "كل الموظفين", "All Employees" },
            { "عقود", "Contracts" },
            { "العطل", "Holidays" },
            { "الاجازات (أدمن)", "Leaves (Admin)" },
            { "إعدادات الاجازات", "Leave Settings" },
            { "الحضور (موظف)", "Attendance (Employee)" },
            { "الأقسام", "Departments" },
            { "التسميات", "Designations" },
            { "الجدول الزمني", "Timesheet" },
            { "جدول المناوبة", "Schedule" },
            { "الوقت الاضافي", "Overtime" },
            {"ورديات","Shifts"},
            { "العملاء", "Clients" },
            { "المشاريع", "Projects" },
            { "مهام", "Tasks" },
            { "لوحة المهام", "Task Board" },
            { "التذاكر", "Tickets" },
            { "تقدير", "Estimate" },
            { "الفواتير", "Invoices" },
            { "المدفوعات", "Payments" },
            { "نفقات", "Expenses" },
            { "صندوق الادخار", "Provident Fund" },
            { "الضرائب", "Taxes" },
            { "فئات", "Categories" },
            { "الميزانيات", "Budgets" },
            { "نفقات الميزانيات", "Budgets Expenses" },
            { "إيرادات الميزانيات", "Budgets Revenues" },
            { "رواتب الموظفين", "Employee Salary" },
            { "عناصر الرواتب", "Payroll Overtime" },
            { "خصومات الرواتب", "Payroll Deduction" },
            { "إضافات الرواتب", "Payroll Addition" },
            { "سياسات", "Policies" },
            { "تقرير يومي", "Daily Report" },
            { "مؤشر الأداء", "Performance Indicator" },
            { "مراجعة الأداء", "Performance Appraisal" },
            { "قائمة الأهداف", "Goal List" },
            { "نوع الهدف", "Goal Type" },
            { "قائمة التدريب", "Training List" },
            { "المدربون", "Trainers" },
            { "نوع التدريب", "Training Type" },
            { "ترقية", "Promotion" },
            { "استقالة", "Resignation" },
            { "فسخ العقد", "Termination" },
            { "أصول", "Assets" },
            { "إدارة الوظائف", "Manage Jobs" },
            { "إدارة السيرة الذاتية", "Job Applicant" },
            { "قائمة المرشحين المختصرة", "Shortlist Candidaties" },
            { "اسئلة المقابلة", "Interview Questions" },
            { "الموافقات على العروض", "Offer Approvals" },
            { "مستوى الخبرة", "Experience Level" },
            { "قائمة المرشحين", "Candidaties List" },
            { "توقيت الجدول الزمني", "Schedule Timing" },
            { "نتائج الكفاءة", "Aptitude Results" },
            { "المستخدمين", "Users" },
            { "الشركات", "Companies" },
            { "الرخص", "Licenses" },
            { "الاشعارات", "Notifications" }
    
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_AppPages_AppPagesId",
                table: "AuditLogs");

            migrationBuilder.DropTable(
                name: "AppPages");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_AppPagesId",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "AppPagesId",
                table: "AuditLogs");

            migrationBuilder.AddColumn<string>(
                name: "PageName",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
