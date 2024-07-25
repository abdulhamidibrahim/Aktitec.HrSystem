using System.Text;
using Aktitic.HrProject.Api.Configuration;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.AutoMapper;
using Aktitic.HrProject.BL.SignalR;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Configuration;
using Aktitic.HrProject.DAL.Helpers.Connection_Strings;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Repos.ClientRepo;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.Repos.GoalListRepo;
using Aktitic.HrProject.DAL.Repos.InvoiceRepo;
using Aktitic.HrProject.DAL.Repos.TrainingListRepo;
using Aktitic.HrProject.DAL.UnitOfWork;
using Aktitic.HrTask.BL;
using Aktitic.HrTaskBoard.BL;
using Aktitic.HrTaskList.BL;
using Aktitic.HrTicket.BL;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using User.Management.Services.Models;
using User.Management.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
    options.CustomSchemaIds(e => e.FullName);
    
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

//add connection string

builder.Services.AddDbContext<HrSystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(HrManagementDbConnection))
        // option => option.CommandTimeout(300)
        );
    // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<HrSystemDbContext>()
    .AddDefaultTokenProviders();


// builder.Services.AddIdentityCore<Employee>()    
//     .AddRoles<IdentityRole<int>>()
//     .AddEntityFrameworkStores<HrManagementDbContext>()
//     .AddDefaultTokenProviders();

var jwtOptions = builder.Configuration.GetSection("JWT").Get<JwtOptions>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // if not valid redirect to login form 
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions?.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions?.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
    };
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
});


var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfiguration);

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
//     options.AddPolicy("User", policy => policy.RequireRole("User"));
// });

// builder.Services.AddCors(
//     
//     options =>
//     {
//         options.AddPolicy("CorsPolicy",
//             b =>
//             {
//                 b.AllowAnyOrigin()
//                     .AllowAnyMethod()
//                     .AllowAnyHeader();
//             });
//     });
    
builder.Services.AddCors(options =>      // cross-origin resource sharing
{
    options.AddPolicy("AllowAngularOrigins",
        policyBuilder =>
        {
            policyBuilder.WithOrigins(
                    "http://localhost:4200"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddAutoMapper(typeof(Program));

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfiles());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddScoped<UserUtility>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<JwtOptions>();
builder.Services.AddSingleton<IEmailService, EmailService>();

#region resolving dependencies
builder.Services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
builder.Services.AddScoped<IAttendanceManager, AttendanceManager>();
builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
builder.Services.AddScoped<IDesignationManager, DesignationManager>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
builder.Services.AddScoped<IHolidayManager, HolidayManager>(); 
builder.Services.AddScoped<ILeavesManager, LeavesManager>();
builder.Services.AddScoped<ILeaveSettingsManager, LeaveSettingsManager>();
builder.Services.AddScoped<IOvertimeManager, OvertimeManager>();
builder.Services.AddScoped<ISchedulingManager, SchedulingManager>();
builder.Services.AddScoped<IShiftManager, ShiftManager>();
builder.Services.AddScoped<ITimesheetManager, TimesheetManager>();
builder.Services.AddScoped<IFileManager, FileManager>();
builder.Services.AddScoped<INoteManager, NoteManager>();
builder.Services.AddScoped<IClientManager, ClientManager>();
builder.Services.AddScoped<ICustomPolicyManager, CustomPolicyManager>();
builder.Services.AddScoped<ITaskManager, TaskManager>();
builder.Services.AddScoped<ITaskListManager, TaskListManager>();
builder.Services.AddScoped<ITaskBoardManager, TaskBoardManager>();
builder.Services.AddScoped<IProjectManager, ProjectManager>();
builder.Services.AddScoped<ITicketManager, TicketManager>();
builder.Services.AddScoped<ITicketFollowersManager, TicketFollowersManager>();
builder.Services.AddScoped<IEstimateManager, EstimateManager>();
builder.Services.AddScoped<IExpensesManager, ExpensesManager>();
builder.Services.AddScoped<IInvoiceManager, InvoiceManager>();
builder.Services.AddScoped<IPaymentManager, PaymentManager>();
builder.Services.AddScoped<ITaxManager, TaxManager>();
builder.Services.AddScoped<IProvidentFundsManager, ProvidentFundsManager>();
builder.Services.AddScoped<IBudgetRevenuesManager, BudgetRevenuesManager>();
builder.Services.AddScoped<IBudgetExpensesManager, BudgetExpensesManager>();
builder.Services.AddScoped<IBudgetManager, BudgetManager>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<IPoliciesManager, PoliciesManager>();
builder.Services.AddScoped<ISalaryManager, SalaryManager>();
builder.Services.AddScoped<IPayrollOvertimeManager, PayrollOvertimeManager>();
builder.Services.AddScoped<IPayrollDeductionManager, PayrollDeductionManager>();
builder.Services.AddScoped<IPayrollAdditionManager, PayrollAdditionManager>();
builder.Services.AddScoped<ITerminationManager, TerminationManager>();
builder.Services.AddScoped<IResignationManager, ResignationManager>();
builder.Services.AddScoped<IPromotionManager, PromotionManager>();
builder.Services.AddScoped<ITrainerManager, TrainerManager>();
builder.Services.AddScoped<ITrainingTypeManager, TrainingTypeManager>();
builder.Services.AddScoped<ITrainingListManager, TrainingListManager>();
builder.Services.AddScoped<IGoalTypeManager, GoalTypeManager>();
builder.Services.AddScoped<IGoalListManager, GoalListManager>();
builder.Services.AddScoped<IPerformanceAppraisalManager, PerformanceAppraisalManager>();
builder.Services.AddScoped<IPerformanceIndicatorManager, PerformanceIndicatorManager>();
builder.Services.AddScoped<IEventManager, EventManager>();
builder.Services.AddScoped<IContactsManager, ContactsManager>();
builder.Services.AddScoped<IContractsManager, ContractsManager>();

#endregion


builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue; //104857600; // 100MB
    options.MemoryBufferThreshold = int.MaxValue;
});

// builder.Services.AddMigrate();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowAngularOrigins");

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();
