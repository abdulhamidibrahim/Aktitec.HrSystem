using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aktitic.HrProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "employee");

            migrationBuilder.EnsureSchema(
                name: "project");

            migrationBuilder.EnsureSchema(
                name: "client");

            migrationBuilder.CreateTable(
                name: "LogActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogActions", x => x.Id);
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
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    HasAccess = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_ApplicationUser_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Warranty = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    OverallExpense = table.Column<float>(type: "real", nullable: true),
                    OverallRevenue = table.Column<float>(type: "real", nullable: true),
                    ExpectedProfit = table.Column<float>(type: "real", nullable: true),
                    Tax = table.Column<float>(type: "real", nullable: true),
                    BudgetAmount = table.Column<float>(type: "real", nullable: true),
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
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubcategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_ChatGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatGroups_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Companies_TenantId",
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
                    UserId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_CompanyRoles_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
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
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    ReceiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Bcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Archive = table.Column<bool>(type: "bit", nullable: false),
                    Starred = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false),
                    Trash = table.Column<bool>(type: "bit", nullable: false),
                    Selected = table.Column<bool>(type: "bit", nullable: false),
                    Spam = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_ApplicationUser_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Emails_ApplicationUser_SenderId",
                        column: x => x.SenderId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Emails_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StarDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceLevel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FamilyInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoB = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_FamilyInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyInformation_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyInformation_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoalTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_GoalTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoalTypes_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Holiday",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
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
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holiday_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeaveSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnualDays = table.Column<int>(type: "int", nullable: false),
                    AnnualCarryForward = table.Column<bool>(type: "bit", nullable: false),
                    AnnualCarryForwardMax = table.Column<int>(type: "int", nullable: false),
                    AnnualEarnedLeave = table.Column<bool>(type: "bit", nullable: false),
                    AnnualActive = table.Column<bool>(type: "bit", nullable: false),
                    SickDays = table.Column<int>(type: "int", nullable: false),
                    SickActive = table.Column<bool>(type: "bit", nullable: false),
                    HospitalisationDays = table.Column<int>(type: "int", nullable: false),
                    HospitalisationActive = table.Column<bool>(type: "bit", nullable: false),
                    MaternityDays = table.Column<int>(type: "int", nullable: false),
                    MaternityActive = table.Column<bool>(type: "bit", nullable: false),
                    PaternityDays = table.Column<int>(type: "int", nullable: false),
                    PaternityActive = table.Column<bool>(type: "bit", nullable: false),
                    LopDays = table.Column<int>(type: "int", nullable: false),
                    LopCarryForward = table.Column<bool>(type: "bit", nullable: false),
                    LopCarryForwardMax = table.Column<int>(type: "int", nullable: false),
                    LopEarnedLeave = table.Column<bool>(type: "bit", nullable: false),
                    LopActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_LeaveSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveSettings_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licenses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licenses_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsAll = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PayrollOvertimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.PrimaryKey("PK_PayrollOvertimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollOvertimes_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percentage = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_Taxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taxes_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_TrainingTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingTypes_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpensesOfBudgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpensesAmount = table.Column<float>(type: "real", nullable: true),
                    ExpensesTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_ExpensesOfBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpensesOfBudgets_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpensesOfBudgets_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Revenues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevenueTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RevenueAmount = table.Column<float>(type: "real", nullable: true),
                    BudgetId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Revenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revenues_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revenues_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetsExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Subcategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_BudgetsExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetsExpenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetsExpenses_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetsRevenues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Subcategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_BudgetsRevenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetsRevenues_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetsRevenues_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatGroupUsers",
                columns: table => new
                {
                    ChatGroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_ChatGroupUsers", x => new { x.ChatGroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ChatGroupUsers_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatGroupUsers_ChatGroups_ChatGroupId",
                        column: x => x.ChatGroupId,
                        principalTable: "ChatGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatGroupUsers_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: true),
                    Write = table.Column<bool>(type: "bit", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    Create = table.Column<bool>(type: "bit", nullable: true),
                    Import = table.Column<bool>(type: "bit", nullable: true),
                    Export = table.Column<bool>(type: "bit", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "project",
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Permissions_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachments_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetAchievement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_GoalLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoalLists_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GoalLists_GoalTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "GoalTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceivedNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ReceivedNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceivedNotifications_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivedNotifications_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyModuleId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "CompanyModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    AppModuleId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_CompanyModules_AppModules_AppModuleId",
                        column: x => x.AppModuleId,
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
                name: "AppPages",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppSubModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPages", x => x.Code);
                    table.ForeignKey(
                        name: "FK_AppPages_AppSubModules_AppSubModuleId",
                        column: x => x.AppSubModuleId,
                        principalTable: "AppSubModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Changes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppPagesCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AppPagesId = table.Column<int>(type: "int", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_AppPages_AppPagesCode",
                        column: x => x.AppPagesCode,
                        principalTable: "AppPages",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_AuditLogs_LogActions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "LogActions",
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
                    CompanyRoleId = table.Column<int>(type: "int", nullable: false),
                    CompanyModuleId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_RolePermissions_CompanyModules_CompanyModuleId",
                        column: x => x.CompanyModuleId,
                        principalTable: "CompanyModules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_CompanyRoles_CompanyRoleId",
                        column: x => x.CompanyRoleId,
                        principalTable: "CompanyRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AptitudeResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    CategoryWiseMark = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TotalMark = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_AptitudeResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AptitudeResults_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
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
                    production = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    @break = table.Column<string>(name: "break", type: "nvarchar(max)", nullable: true),
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    Overtime = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
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
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    SalaryStructureType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ContractEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    JobPosition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractSchedule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomPolicy",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Days = table.Column<short>(type: "smallint", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_CustomPolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomPolicy_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    department_id = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Designation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designation_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img_id = table.Column<int>(type: "int", nullable: true),
                    PublicKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivateKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    age = table.Column<byte>(type: "tinyint", nullable: true),
                    job_position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    joining_date = table.Column<DateOnly>(type: "date", nullable: true),
                    years_of_experience = table.Column<byte>(type: "tinyint", nullable: true),
                    salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamLeader = table.Column<bool>(type: "bit", nullable: true),
                    department_id = table.Column<int>(type: "int", nullable: true),
                    manager_id = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Department_1",
                        column: x => x.department_id,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Employee_manager_id",
                        column: x => x.manager_id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InterviewQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Question = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OptionA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OptionB = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OptionC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OptionD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CorrectAnswer = table.Column<int>(type: "int", nullable: false),
                    CodeSnippets = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AnswerExplanation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VideoLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_InterviewQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewQuestions_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InterviewQuestions_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    JobLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NoOfVacancies = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SalaryFrom = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    SalaryTo = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Polices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
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
                    table.PrimaryKey("PK_Polices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polices_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Polices_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                schema: "client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    purchase_from = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    purchase_date = table.Column<DateOnly>(type: "date", nullable: true),
                    PurchasedById = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<float>(type: "real", nullable: true),
                    paid_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Employee_PurchasedById",
                        column: x => x.PurchasedById,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
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
                    approvedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_leaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leaves_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
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
                name: "Notes",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender_id = table.Column<int>(type: "int", nullable: true),
                    receiver_id = table.Column<int>(type: "int", nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    starred = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_Employee_receiver_id",
                        column: x => x.receiver_id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_Employee_sender_id",
                        column: x => x.sender_id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Overtimes",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ot_date = table.Column<DateOnly>(type: "date", nullable: true),
                    ot_hours = table.Column<byte>(type: "tinyint", nullable: true),
                    ot_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    approvedBy = table.Column<int>(type: "int", nullable: true),
                    employee_id = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Overtimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Overtimes_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
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
                name: "PayrollAdditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    UnitCalculation = table.Column<bool>(type: "bit", nullable: true),
                    Assignee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    UnitAmount = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_PayrollAdditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollAdditions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayrollAdditions_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayrollAdditions_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PayrollDeductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitCalculation = table.Column<bool>(type: "bit", nullable: false),
                    Assignee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    UnitAmount = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PayrollDeductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollDeductions_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayrollDeductions_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceAppraisals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_PerformanceAppraisals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceAppraisals_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerformanceAppraisals_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceIndicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    AddedById = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marketing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Management = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Administration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentationSkill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualityOfWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Efficiency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Integrity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Professionalism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriticalThinking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConflictManagement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attendance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetDeadline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_PerformanceIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceIndicators_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerformanceIndicators_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerformanceIndicators_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalSchema: "employee",
                        principalTable: "Designation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerformanceIndicators_Employee_AddedById",
                        column: x => x.AddedById,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    rate_select = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    rate = table.Column<string>(type: "nvarchar(max)", precision: 5, scale: 2, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    @checked = table.Column<bool>(name: "checked", type: "bit", nullable: true),
                    LeaderId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskBoardId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Client",
                        column: x => x.ClientId,
                        principalSchema: "project",
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_Employee",
                        column: x => x.LeaderId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PromotionFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromotionToId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
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
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotions_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Promotions_Designation_PromotionToId",
                        column: x => x.PromotionToId,
                        principalSchema: "employee",
                        principalTable: "Designation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Promotions_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProvidentFunds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvidentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeShareAmount = table.Column<double>(type: "float", nullable: true),
                    OrganizationShareAmount = table.Column<double>(type: "float", nullable: true),
                    EmployeeSharePercentage = table.Column<double>(type: "float", nullable: true),
                    OrganizationSharePercentage = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_ProvidentFunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvidentFunds_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProvidentFunds_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Resignations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoticeDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ResignationDate = table.Column<DateOnly>(type: "date", nullable: true),
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
                    table.PrimaryKey("PK_Resignations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resignations_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Resignations_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    NetSalary = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    BasicEarnings = table.Column<double>(type: "float", nullable: true),
                    Tds = table.Column<double>(type: "float", nullable: true),
                    Da = table.Column<double>(type: "float", nullable: true),
                    Esi = table.Column<double>(type: "float", nullable: true),
                    Hra = table.Column<double>(type: "float", nullable: true),
                    Pf = table.Column<double>(type: "float", nullable: true),
                    Conveyance = table.Column<double>(type: "float", nullable: true),
                    Leave = table.Column<double>(type: "float", nullable: true),
                    Allowance = table.Column<double>(type: "float", nullable: true),
                    ProfTax = table.Column<double>(type: "float", nullable: true),
                    MedicalAllowance = table.Column<double>(type: "float", nullable: true),
                    LabourWelfare = table.Column<double>(type: "float", nullable: true),
                    Fund = table.Column<double>(type: "float", nullable: true),
                    Others1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayslipId = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Salaries_Employee_EmployeeId",
                        column: x => x.EmployeeId,
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
                    ExtraHours = table.Column<bool>(type: "bit", nullable: true),
                    Publish = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_scheduling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_scheduling_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_scheduling_Department",
                        column: x => x.department_Id,
                        principalSchema: "employee",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_scheduling_Employee_employee_id",
                        column: x => x.employee_id,
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
                    Days = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    approvedBy = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Shift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shift_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shift_Employee",
                        column: x => x.approvedBy,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Terminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoticeDate = table.Column<DateOnly>(type: "date", nullable: true),
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
                    table.PrimaryKey("PK_Terminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminations_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Terminations_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    TicketId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Followers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastReply = table.Column<DateOnly>(type: "date", nullable: true),
                    assigned_to = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    client_id = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Clients",
                        column: x => x.client_id,
                        principalSchema: "project",
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Employee_AssignedTo",
                        column: x => x.assigned_to,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Employee_CreatedBy",
                        column: x => x.created_by,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingTypeId = table.Column<int>(type: "int", nullable: true),
                    TrainerId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_TrainingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingLists_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainingLists_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainingLists_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainingLists_TrainingTypes_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalTable: "TrainingTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobApplicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Resume = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_JobApplicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplicants_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobApplicants_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Pay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AnnualIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LongTermIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_OfferApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferApprovals_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferApprovals_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferApprovals_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTimings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    ScheduleDate1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduleDate2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduleDate3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SelectTime1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SelectTime2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SelectTime3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_ScheduleTimings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleTimings_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduleTimings_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleTimings_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shortlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_Shortlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shortlists_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shortlists_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shortlists_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_EmployeeProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estimate",
                schema: "client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    client_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    billing_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estimate_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    other_information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    estimate_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    total_amount = table.Column<float>(type: "real", nullable: true),
                    discount = table.Column<float>(type: "real", nullable: true),
                    tax = table.Column<float>(type: "real", nullable: true),
                    grand_total = table.Column<float>(type: "real", nullable: true),
                    client_id = table.Column<int>(type: "int", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Estimate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estimate_Client_client_id",
                        column: x => x.client_id,
                        principalSchema: "project",
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Estimate_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Estimate_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tax = table.Column<float>(type: "real", nullable: true),
                    client_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    billing_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    invoice_date = table.Column<DateOnly>(type: "date", nullable: true),
                    due_date = table.Column<DateOnly>(type: "date", nullable: true),
                    other_information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    invoice_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    total_amount = table.Column<float>(type: "real", nullable: true),
                    discount = table.Column<float>(type: "real", nullable: true),
                    grand_total = table.Column<float>(type: "real", nullable: true),
                    client_id = table.Column<int>(type: "int", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Client_client_id",
                        column: x => x.client_id,
                        principalSchema: "project",
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    completed = table.Column<bool>(type: "bit", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    AssignedTo = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task_Employee_AssignedTo",
                        column: x => x.AssignedTo,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Taskboard",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Taskboard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_TaskBoard",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Taskboard_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Timesheet",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    deadline = table.Column<DateOnly>(type: "date", nullable: true),
                    assigned_hours = table.Column<short>(type: "smallint", nullable: true),
                    hours = table.Column<short>(type: "smallint", nullable: true),
                    employee_id = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Timesheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheet_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Timesheet_Employee",
                        column: x => x.employee_id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Timesheet_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<int>(type: "int", nullable: false),
                    First = table.Column<int>(type: "int", nullable: false),
                    Last = table.Column<int>(type: "int", nullable: false),
                    Next = table.Column<int>(type: "int", nullable: true),
                    Previous = table.Column<int>(type: "int", nullable: true),
                    FilesHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revision = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Confidential = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    ExpensesId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalSchema: "client",
                        principalTable: "Expenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "project",
                        principalTable: "Ticket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketFollowers",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_id = table.Column<int>(type: "int", nullable: true),
                    ticket_id = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_TicketFollowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketFollowers_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketFollowers_Employee_employee_id",
                        column: x => x.employee_id,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketFollowers_Ticket",
                        column: x => x.ticket_id,
                        principalSchema: "project",
                        principalTable: "Ticket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<float>(type: "real", nullable: true),
                    UnitCost = table.Column<float>(type: "real", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    EstimateId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Estimate_EstimateId",
                        column: x => x.EstimateId,
                        principalSchema: "client",
                        principalTable: "Estimate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "client",
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaidDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PaidAmount = table.Column<float>(type: "real", nullable: true),
                    TotalAmount = table.Column<float>(type: "real", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "project",
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "client",
                        principalTable: "Invoice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_ApplicationUser_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_ApplicationUser_SenderId",
                        column: x => x.SenderId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_ChatGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ChatGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Task_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "project",
                        principalTable: "Task",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskList",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    TaskBoardId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_TaskList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskList_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskList_Task_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "project",
                        principalTable: "Task",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskList_Taskboard_TaskBoardId",
                        column: x => x.TaskBoardId,
                        principalSchema: "project",
                        principalTable: "Taskboard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    FileHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileNumber = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_DocumentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentFiles_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentFiles_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    FileUserId = table.Column<int>(type: "int", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: true),
                    Write = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_FileUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileUsers_ApplicationUser_FileUserId",
                        column: x => x.FileUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileUsers_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FileUsers_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Revisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DigitalSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Revisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revisors_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Revisors_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revisors_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    CompanyModuleId = table.Column<int>(type: "int", nullable: true),
                    CompanyRoleId = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    ContractId = table.Column<int>(type: "int", nullable: true),
                    CustomPolicyId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    DesignationId = table.Column<int>(type: "int", nullable: true),
                    DocumentFileId = table.Column<int>(type: "int", nullable: true),
                    DocumentId = table.Column<int>(type: "int", nullable: true),
                    EmailId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeProjectsId = table.Column<int>(type: "int", nullable: true),
                    EstimateId = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    ExpensesId = table.Column<int>(type: "int", nullable: true),
                    ExpensesOfBudgetId = table.Column<int>(type: "int", nullable: true),
                    ExperienceId = table.Column<int>(type: "int", nullable: true),
                    FamilyInformationId = table.Column<int>(type: "int", nullable: true),
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
                    MailAttachmentId = table.Column<int>(type: "int", nullable: true),
                    MessageId = table.Column<int>(type: "int", nullable: true),
                    NotesId = table.Column<int>(type: "int", nullable: true),
                    NotificationId = table.Column<int>(type: "int", nullable: true),
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
                    RevisorId = table.Column<int>(type: "int", nullable: true),
                    RolePermissionsId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_ModifiedRecord_Attachments_MailAttachmentId",
                        column: x => x.MailAttachmentId,
                        principalTable: "Attachments",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_ModifiedRecord_CompanyModules_CompanyModuleId",
                        column: x => x.CompanyModuleId,
                        principalTable: "CompanyModules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_CompanyRoles_CompanyRoleId",
                        column: x => x.CompanyRoleId,
                        principalTable: "CompanyRoles",
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
                        name: "FK_ModifiedRecord_DocumentFiles_DocumentFileId",
                        column: x => x.DocumentFileId,
                        principalTable: "DocumentFiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
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
                        name: "FK_ModifiedRecord_FamilyInformation_FamilyInformationId",
                        column: x => x.FamilyInformationId,
                        principalTable: "FamilyInformation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_FileUsers_FileUsersId",
                        column: x => x.FileUsersId,
                        principalTable: "FileUsers",
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
                        name: "FK_ModifiedRecord_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
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
                        name: "FK_ModifiedRecord_Revisors_RevisorId",
                        column: x => x.RevisorId,
                        principalTable: "Revisors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifiedRecord_RolePermissions_RolePermissionsId",
                        column: x => x.RolePermissionsId,
                        principalTable: "RolePermissions",
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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_EmployeeId",
                table: "ApplicationUser",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_TenantId",
                table: "ApplicationUser",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AppModules_CompanyModuleId",
                table: "AppModules",
                column: "CompanyModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPages_AppSubModuleId",
                table: "AppPages",
                column: "AppSubModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSubModules_AppModuleId",
                table: "AppSubModules",
                column: "AppModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AptitudeResults_EmployeeId",
                table: "AptitudeResults",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AptitudeResults_JobId",
                table: "AptitudeResults",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_AptitudeResults_TenantId",
                table: "AptitudeResults",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_TenantId",
                table: "Assets",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_UserId",
                table: "Assets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_EmailId",
                table: "Attachments",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TenantId",
                table: "Attachments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_employee_id",
                schema: "employee",
                table: "Attendance",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_TenantId",
                schema: "employee",
                table: "Attendance",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ActionId",
                table: "AuditLogs",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_AppPagesCode",
                table: "AuditLogs",
                column: "AppPagesCode");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_TenantId",
                table: "Budgets",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetsExpenses_CategoryId",
                table: "BudgetsExpenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetsExpenses_TenantId",
                table: "BudgetsExpenses",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetsRevenues_CategoryId",
                table: "BudgetsRevenues",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetsRevenues_TenantId",
                table: "BudgetsRevenues",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_EmployeeId",
                table: "Candidates",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_TenantId",
                table: "Candidates",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TenantId",
                table: "Categories",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroups_TenantId",
                table: "ChatGroups",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroupUsers_TenantId",
                table: "ChatGroupUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroupUsers_UserId",
                table: "ChatGroupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_TenantId",
                schema: "project",
                table: "Client",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ManagerId",
                table: "Companies",
                column: "ManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyModules_AppModuleId",
                table: "CompanyModules",
                column: "AppModuleId");

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
                name: "IX_CompanyRoles_UserId",
                table: "CompanyRoles",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TenantId",
                table: "Contacts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DepartmentId",
                table: "Contracts",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EmployeeId",
                table: "Contracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TenantId",
                table: "Contracts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPolicy_EmployeeId",
                schema: "employee",
                table: "CustomPolicy",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPolicy_TenantId",
                schema: "employee",
                table: "CustomPolicy",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ProjectId",
                schema: "employee",
                table: "Department",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_TenantId",
                schema: "employee",
                table: "Department",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_department_id",
                schema: "employee",
                table: "Designation",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_TenantId",
                schema: "employee",
                table: "Designation",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_DocumentId",
                table: "DocumentFiles",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_TenantId",
                table: "DocumentFiles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ExpensesId",
                table: "Documents",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProjectId",
                table: "Documents",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TenantId",
                table: "Documents",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TicketId",
                table: "Documents",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ReceiverId",
                table: "Emails",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_SenderId",
                table: "Emails",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_TenantId",
                table: "Emails",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_department_id",
                schema: "employee",
                table: "Employee",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_manager_id",
                schema: "employee",
                table: "Employee",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_TenantId",
                schema: "employee",
                table: "Employee",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_EmployeeId",
                schema: "project",
                table: "EmployeeProjects",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_ProjectId",
                schema: "project",
                table: "EmployeeProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_TenantId",
                schema: "project",
                table: "EmployeeProjects",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Estimate_client_id",
                schema: "client",
                table: "Estimate",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Estimate_project_id",
                schema: "client",
                table: "Estimate",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Estimate_TenantId",
                schema: "client",
                table: "Estimate",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TenantId",
                table: "Events",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PurchasedById",
                schema: "client",
                table: "Expenses",
                column: "PurchasedById");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_TenantId",
                schema: "client",
                table: "Expenses",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesOfBudgets_BudgetId",
                table: "ExpensesOfBudgets",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesOfBudgets_TenantId",
                table: "ExpensesOfBudgets",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_TenantId",
                table: "Experiences",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyInformation_TenantId",
                table: "FamilyInformation",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyInformation_UserId",
                table: "FamilyInformation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FileUsers_DocumentId",
                table: "FileUsers",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_FileUsers_FileUserId",
                table: "FileUsers",
                column: "FileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FileUsers_TenantId",
                table: "FileUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalLists_TenantId",
                table: "GoalLists",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalLists_TypeId",
                table: "GoalLists",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalTypes_TenantId",
                table: "GoalTypes",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_TenantId",
                schema: "employee",
                table: "Holiday",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewQuestions_DepartmentId",
                table: "InterviewQuestions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewQuestions_TenantId",
                table: "InterviewQuestions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_client_id",
                schema: "client",
                table: "Invoice",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_project_id",
                schema: "client",
                table: "Invoice",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_TenantId",
                schema: "client",
                table: "Invoice",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_EstimateId",
                table: "Items",
                column: "EstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_InvoiceId",
                table: "Items",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TenantId",
                table: "Items",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicants_JobId",
                table: "JobApplicants",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicants_TenantId",
                table: "JobApplicants",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DepartmentId",
                table: "Jobs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_TenantId",
                table: "Jobs",
                column: "TenantId");

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
                name: "IX_leaves_TenantId",
                schema: "employee",
                table: "leaves",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveSettings_TenantId",
                table: "LeaveSettings",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_CompanyId",
                table: "Licenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_TenantId",
                table: "Licenses",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GroupId",
                table: "Messages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TaskId",
                table: "Messages",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TenantId",
                table: "Messages",
                column: "TenantId");

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
                name: "IX_ModifiedRecord_CompanyModuleId",
                table: "ModifiedRecord",
                column: "CompanyModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_CompanyRoleId",
                table: "ModifiedRecord",
                column: "CompanyRoleId");

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
                name: "IX_ModifiedRecord_DocumentFileId",
                table: "ModifiedRecord",
                column: "DocumentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_DocumentId",
                table: "ModifiedRecord",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_EmailId",
                table: "ModifiedRecord",
                column: "EmailId");

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
                name: "IX_ModifiedRecord_FamilyInformationId",
                table: "ModifiedRecord",
                column: "FamilyInformationId");

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
                name: "IX_ModifiedRecord_MailAttachmentId",
                table: "ModifiedRecord",
                column: "MailAttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_MessageId",
                table: "ModifiedRecord",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_NotesId",
                table: "ModifiedRecord",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_NotificationId",
                table: "ModifiedRecord",
                column: "NotificationId");

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
                name: "IX_ModifiedRecord_RevisorId",
                table: "ModifiedRecord",
                column: "RevisorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRecord_RolePermissionsId",
                table: "ModifiedRecord",
                column: "RolePermissionsId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Notes_receiver_id",
                schema: "employee",
                table: "Notes",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_sender_id",
                schema: "employee",
                table: "Notes",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TenantId",
                schema: "employee",
                table: "Notes",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CompanyId",
                table: "Notifications",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TenantId",
                table: "Notifications",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferApprovals_EmployeeId",
                table: "OfferApprovals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferApprovals_JobId",
                table: "OfferApprovals",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferApprovals_TenantId",
                table: "OfferApprovals",
                column: "TenantId");

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
                name: "IX_Overtimes_TenantId",
                schema: "employee",
                table: "Overtimes",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientId",
                table: "Payments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TenantId",
                table: "Payments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_CategoryId",
                table: "PayrollAdditions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_EmployeeId",
                table: "PayrollAdditions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_TenantId",
                table: "PayrollAdditions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductions_EmployeeId",
                table: "PayrollDeductions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductions_TenantId",
                table: "PayrollDeductions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollOvertimes_TenantId",
                table: "PayrollOvertimes",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceAppraisals_EmployeeId",
                table: "PerformanceAppraisals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceAppraisals_TenantId",
                table: "PerformanceAppraisals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceIndicators_AddedById",
                table: "PerformanceIndicators",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceIndicators_DepartmentId",
                table: "PerformanceIndicators",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceIndicators_DesignationId",
                table: "PerformanceIndicators",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceIndicators_TenantId",
                table: "PerformanceIndicators",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ClientId",
                schema: "project",
                table: "Permissions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_TenantId",
                schema: "project",
                table: "Permissions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Polices_DepartmentId",
                table: "Polices",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Polices_TenantId",
                table: "Polices",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClientId",
                schema: "project",
                table: "Project",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LeaderId",
                schema: "project",
                table: "Project",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TenantId",
                schema: "project",
                table: "Project",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_EmployeeId",
                table: "Promotions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_PromotionToId",
                table: "Promotions",
                column: "PromotionToId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_TenantId",
                table: "Promotions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidentFunds_EmployeeId",
                table: "ProvidentFunds",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidentFunds_TenantId",
                table: "ProvidentFunds",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedNotifications_NotificationId",
                table: "ReceivedNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedNotifications_TenantId",
                table: "ReceivedNotifications",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Resignations_EmployeeId",
                table: "Resignations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resignations_TenantId",
                table: "Resignations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_BudgetId",
                table: "Revenues",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_TenantId",
                table: "Revenues",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisors_DocumentId",
                table: "Revisors",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisors_EmployeeId",
                table: "Revisors",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisors_TenantId",
                table: "Revisors",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_CompanyModuleId",
                table: "RolePermissions",
                column: "CompanyModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_CompanyRoleId",
                table: "RolePermissions",
                column: "CompanyRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PageCode",
                table: "RolePermissions",
                column: "PageCode");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_TenantId",
                table: "RolePermissions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_TenantId",
                table: "Salaries",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTimings_EmployeeId",
                table: "ScheduleTimings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTimings_JobId",
                table: "ScheduleTimings",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTimings_TenantId",
                table: "ScheduleTimings",
                column: "TenantId");

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
                name: "IX_scheduling_TenantId",
                schema: "employee",
                table: "scheduling",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_approvedBy",
                schema: "employee",
                table: "Shift",
                column: "approvedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_TenantId",
                schema: "employee",
                table: "Shift",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Shortlists_EmployeeId",
                table: "Shortlists",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shortlists_JobId",
                table: "Shortlists",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Shortlists_TenantId",
                table: "Shortlists",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_AssignedTo",
                schema: "project",
                table: "Task",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Task_project_id",
                schema: "project",
                table: "Task",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TenantId",
                schema: "project",
                table: "Task",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Taskboard_ProjectId",
                schema: "project",
                table: "Taskboard",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Taskboard_TenantId",
                schema: "project",
                table: "Taskboard",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_TaskBoardId",
                schema: "project",
                table: "TaskList",
                column: "TaskBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_TaskId",
                schema: "project",
                table: "TaskList",
                column: "TaskId",
                unique: true,
                filter: "[TaskId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_TenantId",
                schema: "project",
                table: "TaskList",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_TenantId",
                table: "Taxes",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminations_EmployeeId",
                table: "Terminations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminations_TenantId",
                table: "Terminations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_assigned_to",
                schema: "project",
                table: "Ticket",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_client_id",
                schema: "project",
                table: "Ticket",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_created_by",
                schema: "project",
                table: "Ticket",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TenantId",
                schema: "project",
                table: "Ticket",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFollowers_employee_id",
                schema: "project",
                table: "TicketFollowers",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFollowers_TenantId",
                schema: "project",
                table: "TicketFollowers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFollowers_ticket_id",
                schema: "project",
                table: "TicketFollowers",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheet_employee_id",
                schema: "employee",
                table: "Timesheet",
                column: "employee_id",
                unique: true,
                filter: "[employee_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheet_project_id",
                schema: "employee",
                table: "Timesheet",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheet_TenantId",
                schema: "employee",
                table: "Timesheet",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_TenantId",
                table: "Trainers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingLists_EmployeeId",
                table: "TrainingLists",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingLists_TenantId",
                table: "TrainingLists",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingLists_TrainerId",
                table: "TrainingLists",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingLists_TrainingTypeId",
                table: "TrainingLists",
                column: "TrainingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTypes_TenantId",
                table: "TrainingTypes",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Companies_TenantId",
                table: "ApplicationUser",
                column: "TenantId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Employee_EmployeeId",
                table: "ApplicationUser",
                column: "EmployeeId",
                principalSchema: "employee",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppModules_CompanyModules_CompanyModuleId",
                table: "AppModules",
                column: "CompanyModuleId",
                principalTable: "CompanyModules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AptitudeResults_Employee_EmployeeId",
                table: "AptitudeResults",
                column: "EmployeeId",
                principalSchema: "employee",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AptitudeResults_Jobs_JobId",
                table: "AptitudeResults",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Employee",
                schema: "employee",
                table: "Attendance",
                column: "employee_id",
                principalSchema: "employee",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Employee_EmployeeId",
                table: "Candidates",
                column: "EmployeeId",
                principalSchema: "employee",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Department_DepartmentId",
                table: "Contracts",
                column: "DepartmentId",
                principalSchema: "employee",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Employee_EmployeeId",
                table: "Contracts",
                column: "EmployeeId",
                principalSchema: "employee",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomPolicy_Employee_EmployeeId",
                schema: "employee",
                table: "CustomPolicy",
                column: "EmployeeId",
                principalSchema: "employee",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Project_ProjectId",
                schema: "employee",
                table: "Department",
                column: "ProjectId",
                principalSchema: "project",
                principalTable: "Project",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Companies_TenantId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Companies_TenantId",
                schema: "project",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyModules_Companies_CompanyId",
                table: "CompanyModules");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyModules_Companies_TenantId",
                table: "CompanyModules");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Companies_TenantId",
                schema: "employee",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Companies_TenantId",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Companies_TenantId",
                schema: "project",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Employee",
                schema: "project",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_AppModules_CompanyModules_CompanyModuleId",
                table: "AppModules");

            migrationBuilder.DropTable(
                name: "ModifiedRecord");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "AptitudeResults");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Attendance",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BudgetsExpenses");

            migrationBuilder.DropTable(
                name: "BudgetsRevenues");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "ChatGroupUsers");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "CustomPolicy",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "DocumentFiles");

            migrationBuilder.DropTable(
                name: "EmployeeProjects",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "ExpensesOfBudgets");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "FamilyInformation");

            migrationBuilder.DropTable(
                name: "FileUsers");

            migrationBuilder.DropTable(
                name: "GoalLists");

            migrationBuilder.DropTable(
                name: "Holiday",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "InterviewQuestions");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "JobApplicants");

            migrationBuilder.DropTable(
                name: "LeaveSettings");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notes",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "OfferApprovals");

            migrationBuilder.DropTable(
                name: "Overtimes",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PayrollAdditions");

            migrationBuilder.DropTable(
                name: "PayrollDeductions");

            migrationBuilder.DropTable(
                name: "PayrollOvertimes");

            migrationBuilder.DropTable(
                name: "PerformanceAppraisals");

            migrationBuilder.DropTable(
                name: "PerformanceIndicators");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Polices");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "ProvidentFunds");

            migrationBuilder.DropTable(
                name: "ReceivedNotifications");

            migrationBuilder.DropTable(
                name: "Resignations");

            migrationBuilder.DropTable(
                name: "Revenues");

            migrationBuilder.DropTable(
                name: "Revisors");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "ScheduleTimings");

            migrationBuilder.DropTable(
                name: "Shift",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Shortlists");

            migrationBuilder.DropTable(
                name: "TaskList",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "Terminations");

            migrationBuilder.DropTable(
                name: "TicketFollowers",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Timesheet",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "TrainingLists");

            migrationBuilder.DropTable(
                name: "leaves",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "scheduling",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "LogActions");

            migrationBuilder.DropTable(
                name: "GoalTypes");

            migrationBuilder.DropTable(
                name: "Estimate",
                schema: "client");

            migrationBuilder.DropTable(
                name: "ChatGroups");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "client");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Designation",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "AppPages");

            migrationBuilder.DropTable(
                name: "CompanyRoles");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Task",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Taskboard",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "TrainingTypes");

            migrationBuilder.DropTable(
                name: "Expenses",
                schema: "client");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "project");

            migrationBuilder.DropTable(
                name: "AppSubModules");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "project");

            migrationBuilder.DropTable(
                name: "CompanyModules");

            migrationBuilder.DropTable(
                name: "AppModules");
        }
    }
}
