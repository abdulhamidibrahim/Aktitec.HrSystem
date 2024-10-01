using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Taskboard_TaskBoardId",
                schema: "project",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Taskboard_Project_ProjectId",
                schema: "project",
                table: "Taskboard");

            migrationBuilder.DropIndex(
                name: "IX_Taskboard_ProjectId",
                schema: "project",
                table: "Taskboard");

            migrationBuilder.DropIndex(
                name: "IX_Project_TaskBoardId",
                schema: "project",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "ListName",
                schema: "project",
                table: "Taskboard",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                schema: "project",
                table: "Taskboard",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PageName",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModifiedRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditLogId = table.Column<int>(type: "int", nullable: false),
                    RecordId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermenantlyDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PermenantlyDeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AptitudeResultId = table.Column<int>(type: "int", nullable: true),
                    AssetId = table.Column<int>(type: "int", nullable: true),
                    AttendanceId = table.Column<int>(type: "int", nullable: true),
                    BudgetExpensesId = table.Column<int>(type: "int", nullable: true),
                    BudgetId = table.Column<int>(type: "int", nullable: true),
                    BudgetRevenueId = table.Column<int>(type: "int", nullable: true),
                    CandidateId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ChatGroupId = table.Column<int>(type: "int", nullable: true),
                    ChatGroupUserChatGroupId = table.Column<int>(type: "int", nullable: true),
                    ChatGroupUserUserId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    ContractId = table.Column<int>(type: "int", nullable: true),
                    CustomPolicyId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    DesignationId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeProjectsId = table.Column<int>(type: "int", nullable: true),
                    EstimateId = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    ExpensesId = table.Column<int>(type: "int", nullable: true),
                    ExpensesOfBudgetId = table.Column<int>(type: "int", nullable: true),
                    ExperienceId = table.Column<int>(type: "int", nullable: true),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    FileUsersId = table.Column<int>(type: "int", nullable: true),
                    GoalListId = table.Column<int>(type: "int", nullable: true),
                    GoalTypeId = table.Column<int>(type: "int", nullable: true),
                    HolidayId = table.Column<int>(type: "int", nullable: true),
                    InterviewQuestionId = table.Column<int>(type: "int", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    JobApplicantId = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    LeaveSettingsId = table.Column<int>(type: "int", nullable: true),
                    LeavesId = table.Column<int>(type: "int", nullable: true),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    MessageId = table.Column<int>(type: "int", nullable: true),
                    NotesId = table.Column<int>(type: "int", nullable: true),
                    OfferApprovalId = table.Column<int>(type: "int", nullable: true),
                    OvertimeId = table.Column<int>(type: "int", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    PayrollAdditionId = table.Column<int>(type: "int", nullable: true),
                    PayrollDeductionId = table.Column<int>(type: "int", nullable: true),
                    PayrollOvertimeId = table.Column<int>(type: "int", nullable: true),
                    PerformanceAppraisalId = table.Column<int>(type: "int", nullable: true),
                    PerformanceIndicatorId = table.Column<int>(type: "int", nullable: true),
                    PermissionId = table.Column<int>(type: "int", nullable: true),
                    PoliciesId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    PromotionId = table.Column<int>(type: "int", nullable: true),
                    ProvidentFundsId = table.Column<int>(type: "int", nullable: true),
                    ReceivedNotificationId = table.Column<int>(type: "int", nullable: true),
                    ResignationId = table.Column<int>(type: "int", nullable: true),
                    RevenueId = table.Column<int>(type: "int", nullable: true),
                    SalaryId = table.Column<int>(type: "int", nullable: true),
                    ScheduleTimingId = table.Column<int>(type: "int", nullable: true),
                    SchedulingId = table.Column<int>(type: "int", nullable: true),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    ShortlistId = table.Column<int>(type: "int", nullable: true),
                    TaskBoardId = table.Column<int>(type: "int", nullable: true),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    TaskListId = table.Column<int>(type: "int", nullable: true),
                    TaxId = table.Column<int>(type: "int", nullable: true),
                    TerminationId = table.Column<int>(type: "int", nullable: true),
                    TicketFollowersId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    TimeSheetId = table.Column<int>(type: "int", nullable: true),
                    TrainerId = table.Column<int>(type: "int", nullable: true),
                    TrainingListId = table.Column<int>(type: "int", nullable: true),
                    TrainingTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifiedRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_AptitudeResults_AptitudeResultId",
                        column: x => x.AptitudeResultId,
                        principalTable: "AptitudeResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Attendance_AttendanceId",
                        column: x => x.AttendanceId,
                        principalSchema: "employee",
                        principalTable: "Attendance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AuditLogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_BudgetsExpenses_BudgetExpensesId",
                        column: x => x.BudgetExpensesId,
                        principalTable: "BudgetsExpenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_BudgetsRevenues_BudgetRevenueId",
                        column: x => x.BudgetRevenueId,
                        principalTable: "BudgetsRevenues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_ChatGroupUsers_ChatGroupUserChatGroupId_ChatGroupUserUserId",
                        columns: x => new { x.ChatGroupUserChatGroupId, x.ChatGroupUserUserId },
                        principalTable: "ChatGroupUsers",
                        principalColumns: new[] { "ChatGroupId", "UserId" });
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_ChatGroups_ChatGroupId",
                        column: x => x.ChatGroupId,
                        principalTable: "ChatGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "project",
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_CustomPolicy_CustomPolicyId",
                        column: x => x.CustomPolicyId,
                        principalSchema: "employee",
                        principalTable: "CustomPolicy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalSchema: "employee",
                        principalTable: "Designation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_EmployeeProjects_EmployeeProjectsId",
                        column: x => x.EmployeeProjectsId,
                        principalSchema: "project",
                        principalTable: "EmployeeProjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Estimate_EstimateId",
                        column: x => x.EstimateId,
                        principalSchema: "client",
                        principalTable: "Estimate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_ExpensesOfBudgets_ExpensesOfBudgetId",
                        column: x => x.ExpensesOfBudgetId,
                        principalTable: "ExpensesOfBudgets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalSchema: "client",
                        principalTable: "Expenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_FileUsers_FileUsersId",
                        column: x => x.FileUsersId,
                        principalTable: "FileUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_File_FileId",
                        column: x => x.FileId,
                        principalSchema: "employee",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_GoalLists_GoalListId",
                        column: x => x.GoalListId,
                        principalTable: "GoalLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_GoalTypes_GoalTypeId",
                        column: x => x.GoalTypeId,
                        principalTable: "GoalTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Holiday_HolidayId",
                        column: x => x.HolidayId,
                        principalSchema: "employee",
                        principalTable: "Holiday",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_InterviewQuestions_InterviewQuestionId",
                        column: x => x.InterviewQuestionId,
                        principalTable: "InterviewQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "client",
                        principalTable: "Invoice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_JobApplicants_JobApplicantId",
                        column: x => x.JobApplicantId,
                        principalTable: "JobApplicants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_LeaveSettings_LeaveSettingsId",
                        column: x => x.LeaveSettingsId,
                        principalTable: "LeaveSettings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Notes_NotesId",
                        column: x => x.NotesId,
                        principalSchema: "employee",
                        principalTable: "Notes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_OfferApprovals_OfferApprovalId",
                        column: x => x.OfferApprovalId,
                        principalTable: "OfferApprovals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Overtimes_OvertimeId",
                        column: x => x.OvertimeId,
                        principalSchema: "employee",
                        principalTable: "Overtimes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_PayrollAdditions_PayrollAdditionId",
                        column: x => x.PayrollAdditionId,
                        principalTable: "PayrollAdditions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_PayrollDeductions_PayrollDeductionId",
                        column: x => x.PayrollDeductionId,
                        principalTable: "PayrollDeductions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_PayrollOvertimes_PayrollOvertimeId",
                        column: x => x.PayrollOvertimeId,
                        principalTable: "PayrollOvertimes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_PerformanceAppraisals_PerformanceAppraisalId",
                        column: x => x.PerformanceAppraisalId,
                        principalTable: "PerformanceAppraisals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_PerformanceIndicators_PerformanceIndicatorId",
                        column: x => x.PerformanceIndicatorId,
                        principalTable: "PerformanceIndicators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "project",
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Polices_PoliciesId",
                        column: x => x.PoliciesId,
                        principalTable: "Polices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_ProvidentFunds_ProvidentFundsId",
                        column: x => x.ProvidentFundsId,
                        principalTable: "ProvidentFunds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_ReceivedNotifications_ReceivedNotificationId",
                        column: x => x.ReceivedNotificationId,
                        principalTable: "ReceivedNotifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Resignations_ResignationId",
                        column: x => x.ResignationId,
                        principalTable: "Resignations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Revenues_RevenueId",
                        column: x => x.RevenueId,
                        principalTable: "Revenues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Salaries_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "Salaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_ScheduleTimings_ScheduleTimingId",
                        column: x => x.ScheduleTimingId,
                        principalTable: "ScheduleTimings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalSchema: "employee",
                        principalTable: "Shift",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Shortlists_ShortlistId",
                        column: x => x.ShortlistId,
                        principalTable: "Shortlists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_TaskList_TaskListId",
                        column: x => x.TaskListId,
                        principalSchema: "project",
                        principalTable: "TaskList",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Task_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "project",
                        principalTable: "Task",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Taskboard_TaskBoardId",
                        column: x => x.TaskBoardId,
                        principalSchema: "project",
                        principalTable: "Taskboard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Terminations_TerminationId",
                        column: x => x.TerminationId,
                        principalTable: "Terminations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_TicketFollowers_TicketFollowersId",
                        column: x => x.TicketFollowersId,
                        principalSchema: "project",
                        principalTable: "TicketFollowers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "project",
                        principalTable: "Ticket",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Timesheet_TimeSheetId",
                        column: x => x.TimeSheetId,
                        principalSchema: "employee",
                        principalTable: "Timesheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_TrainingLists_TrainingListId",
                        column: x => x.TrainingListId,
                        principalTable: "TrainingLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_TrainingTypes_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalTable: "TrainingTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_leaves_LeavesId",
                        column: x => x.LeavesId,
                        principalSchema: "employee",
                        principalTable: "leaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_scheduling_SchedulingId",
                        column: x => x.SchedulingId,
                        principalSchema: "employee",
                        principalTable: "scheduling",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Taskboard_ProjectId",
                schema: "project",
                table: "Taskboard",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_AptitudeResultId",
                table: "ModifiedRecord",
                column: "AptitudeResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_AssetId",
                table: "ModifiedRecord",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_AttendanceId",
                table: "ModifiedRecord",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_AuditLogId",
                table: "ModifiedRecord",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_BudgetExpensesId",
                table: "ModifiedRecord",
                column: "BudgetExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_BudgetId",
                table: "ModifiedRecord",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_BudgetRevenueId",
                table: "ModifiedRecord",
                column: "BudgetRevenueId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_CandidateId",
                table: "ModifiedRecord",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_CategoryId",
                table: "ModifiedRecord",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ChatGroupId",
                table: "ModifiedRecord",
                column: "ChatGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ChatGroupUserChatGroupId_ChatGroupUserUserId",
                table: "ModifiedRecord",
                columns: new[] { "ChatGroupUserChatGroupId", "ChatGroupUserUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ClientId",
                table: "ModifiedRecord",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ContactId",
                table: "ModifiedRecord",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ContractId",
                table: "ModifiedRecord",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_CustomPolicyId",
                table: "ModifiedRecord",
                column: "CustomPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_DepartmentId",
                table: "ModifiedRecord",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_DesignationId",
                table: "ModifiedRecord",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_EmployeeId",
                table: "ModifiedRecord",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_EmployeeProjectsId",
                table: "ModifiedRecord",
                column: "EmployeeProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_EstimateId",
                table: "ModifiedRecord",
                column: "EstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_EventId",
                table: "ModifiedRecord",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ExpensesId",
                table: "ModifiedRecord",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ExpensesOfBudgetId",
                table: "ModifiedRecord",
                column: "ExpensesOfBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ExperienceId",
                table: "ModifiedRecord",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_FileId",
                table: "ModifiedRecord",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_FileUsersId",
                table: "ModifiedRecord",
                column: "FileUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_GoalListId",
                table: "ModifiedRecord",
                column: "GoalListId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_GoalTypeId",
                table: "ModifiedRecord",
                column: "GoalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_HolidayId",
                table: "ModifiedRecord",
                column: "HolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_InterviewQuestionId",
                table: "ModifiedRecord",
                column: "InterviewQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_InvoiceId",
                table: "ModifiedRecord",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ItemId",
                table: "ModifiedRecord",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_JobApplicantId",
                table: "ModifiedRecord",
                column: "JobApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_JobId",
                table: "ModifiedRecord",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_LeaveSettingsId",
                table: "ModifiedRecord",
                column: "LeaveSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_LeavesId",
                table: "ModifiedRecord",
                column: "LeavesId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_LicenseId",
                table: "ModifiedRecord",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_MessageId",
                table: "ModifiedRecord",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_NotesId",
                table: "ModifiedRecord",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_OfferApprovalId",
                table: "ModifiedRecord",
                column: "OfferApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_OvertimeId",
                table: "ModifiedRecord",
                column: "OvertimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_PaymentId",
                table: "ModifiedRecord",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_PayrollAdditionId",
                table: "ModifiedRecord",
                column: "PayrollAdditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_PayrollDeductionId",
                table: "ModifiedRecord",
                column: "PayrollDeductionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_PayrollOvertimeId",
                table: "ModifiedRecord",
                column: "PayrollOvertimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_PerformanceAppraisalId",
                table: "ModifiedRecord",
                column: "PerformanceAppraisalId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_PerformanceIndicatorId",
                table: "ModifiedRecord",
                column: "PerformanceIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_PermissionId",
                table: "ModifiedRecord",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_PoliciesId",
                table: "ModifiedRecord",
                column: "PoliciesId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ProjectId",
                table: "ModifiedRecord",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_PromotionId",
                table: "ModifiedRecord",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ProvidentFundsId",
                table: "ModifiedRecord",
                column: "ProvidentFundsId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ReceivedNotificationId",
                table: "ModifiedRecord",
                column: "ReceivedNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ResignationId",
                table: "ModifiedRecord",
                column: "ResignationId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_RevenueId",
                table: "ModifiedRecord",
                column: "RevenueId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_SalaryId",
                table: "ModifiedRecord",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ScheduleTimingId",
                table: "ModifiedRecord",
                column: "ScheduleTimingId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_SchedulingId",
                table: "ModifiedRecord",
                column: "SchedulingId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ShiftId",
                table: "ModifiedRecord",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_ShortlistId",
                table: "ModifiedRecord",
                column: "ShortlistId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TaskBoardId",
                table: "ModifiedRecord",
                column: "TaskBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TaskId",
                table: "ModifiedRecord",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TaskListId",
                table: "ModifiedRecord",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TaxId",
                table: "ModifiedRecord",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TerminationId",
                table: "ModifiedRecord",
                column: "TerminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TicketFollowersId",
                table: "ModifiedRecord",
                column: "TicketFollowersId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TicketId",
                table: "ModifiedRecord",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TimeSheetId",
                table: "ModifiedRecord",
                column: "TimeSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TrainerId",
                table: "ModifiedRecord",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TrainingListId",
                table: "ModifiedRecord",
                column: "TrainingListId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_TrainingTypeId",
                table: "ModifiedRecord",
                column: "TrainingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_TaskBoard",
                schema: "project",
                table: "Taskboard",
                column: "ProjectId",
                principalSchema: "project",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_TaskBoard",
                schema: "project",
                table: "Taskboard");

            migrationBuilder.DropTable(
                name: "ModifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_Taskboard_ProjectId",
                schema: "project",
                table: "Taskboard");

            migrationBuilder.DropColumn(
                name: "PageName",
                table: "AuditLogs");

            migrationBuilder.AlterColumn<string>(
                name: "ListName",
                schema: "project",
                table: "Taskboard",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                schema: "project",
                table: "Taskboard",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Taskboard_ProjectId",
                schema: "project",
                table: "Taskboard",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TaskBoardId",
                schema: "project",
                table: "Project",
                column: "TaskBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Taskboard_TaskBoardId",
                schema: "project",
                table: "Project",
                column: "TaskBoardId",
                principalSchema: "project",
                principalTable: "Taskboard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Taskboard_Project_ProjectId",
                schema: "project",
                table: "Taskboard",
                column: "ProjectId",
                principalSchema: "project",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
