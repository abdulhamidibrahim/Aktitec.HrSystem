using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "employee");

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "File",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    extension = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holiday",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<int>(type: "int", nullable: true),
                    department_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designation_Department",
                        column: x => x.department_id,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img_id = table.Column<int>(type: "int", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    age = table.Column<byte>(type: "tinyint", nullable: true),
                    job_position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    joining_date = table.Column<DateOnly>(type: "date", nullable: true),
                    years_of_experience = table.Column<byte>(type: "tinyint", nullable: true),
                    salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    department_id = table.Column<int>(type: "int", nullable: true),
                    manager_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Department_1",
                        column: x => x.department_id,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_File_1",
                        column: x => x.img_id,
                        principalSchema: "employee",
                        principalTable: "File",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leaves",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    from = table.Column<DateOnly>(type: "date", nullable: true),
                    to = table.Column<DateOnly>(type: "date", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    days = table.Column<short>(type: "smallint", nullable: true),
                    approved = table.Column<bool>(type: "bit", nullable: true),
                    approvedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leaves_Employee",
                        column: x => x.employee_id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_leaves_Employee_approved",
                        column: x => x.approvedBy,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Overtimes",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ot_date = table.Column<DateOnly>(type: "date", nullable: true),
                    ot_hours = table.Column<byte>(type: "tinyint", nullable: true),
                    ot_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    approvedBy = table.Column<int>(type: "int", nullable: true),
                    employee_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Overtimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Overtimes_Employee",
                        column: x => x.employee_id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Overtimes_Employee_approve",
                        column: x => x.approvedBy,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "scheduling",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_Id = table.Column<int>(type: "int", nullable: true),
                    employee_id = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    shift_id = table.Column<int>(type: "int", nullable: true),
                    min_start_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    max_start_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    start_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    min_end_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    end_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    max_end_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    break_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    repeat_every = table.Column<short>(type: "smallint", nullable: true),
                    note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    approvedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scheduling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_scheduling_Department",
                        column: x => x.department_Id,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_scheduling_Employee_1",
                        column: x => x.employee_id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_scheduling_Employee_2",
                        column: x => x.approvedBy,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    min_start_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    start_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    max_start_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    min_end_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    end_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    max_end_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    breake_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    repeat_every = table.Column<short>(type: "smallint", nullable: true),
                    recurring_shift = table.Column<bool>(type: "bit", nullable: true),
                    indefinate = table.Column<bool>(type: "bit", nullable: true),
                    tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    approvedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shift_Employee",
                        column: x => x.approvedBy,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Timesheet",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    assigned_hours = table.Column<short>(type: "smallint", nullable: true),
                    hours = table.Column<short>(type: "smallint", nullable: true),
                    employee_id = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheet_Employee",
                        column: x => x.Id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    punch_in = table.Column<DateTime>(type: "datetime", nullable: true),
                    punch_out = table.Column<DateTime>(type: "datetime", nullable: true),
                    production = table.Column<TimeOnly>(type: "time", nullable: true),
                    @break = table.Column<TimeOnly>(name: "break", type: "time", nullable: true),
                    overtime_id = table.Column<int>(type: "int", nullable: true),
                    employee_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Employee",
                        column: x => x.employee_id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attendance_Overtimes",
                        column: x => x.overtime_id,
                        principalSchema: "employee",
                        principalTable: "Overtimes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_employee_id",
                schema: "employee",
                table: "Attendance",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_overtime_id",
                schema: "employee",
                table: "Attendance",
                column: "overtime_id");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_department_id",
                schema: "employee",
                table: "Designation",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_department_id",
                schema: "employee",
                table: "Employee",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_img_id",
                schema: "employee",
                table: "Employee",
                column: "img_id");

            migrationBuilder.CreateIndex(
                name: "IX_leaves_approvedBy",
                schema: "employee",
                table: "leaves",
                column: "approvedBy");

            migrationBuilder.CreateIndex(
                name: "IX_leaves_employee_id",
                schema: "employee",
                table: "leaves",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Overtimes_approvedBy",
                schema: "employee",
                table: "Overtimes",
                column: "approvedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Overtimes_employee_id",
                schema: "employee",
                table: "Overtimes",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_scheduling_approvedBy",
                schema: "employee",
                table: "scheduling",
                column: "approvedBy");

            migrationBuilder.CreateIndex(
                name: "IX_scheduling_department_Id",
                schema: "employee",
                table: "scheduling",
                column: "department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_scheduling_employee_id",
                schema: "employee",
                table: "scheduling",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_approvedBy",
                schema: "employee",
                table: "Shift",
                column: "approvedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Designation",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Holiday",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "leaves",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "scheduling",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Shift",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Timesheet",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Overtimes",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "File",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
