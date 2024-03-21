using System.Text;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.AutoMapper;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Repos.ClientRepo;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
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
    // options.CustomSchemaIds(e => e.FullName);
    
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

//add connection string
builder.Services.AddDbContext<HrSystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HrManagementDbConnection"),
        option => option.CommandTimeout(300));
    // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<HrSystemDbContext>()
    .AddDefaultTokenProviders();


// builder.Services.AddIdentityCore<Employee>()    
//     .AddRoles<IdentityRole<int>>()
//     .AddEntityFrameworkStores<HrManagementDbContext>()
//     .AddDefaultTokenProviders();

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
        ValidIssuer = builder.Configuration["JWT:issuer"],
        ValidAudience = builder.Configuration["JWT:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:secret"]!))
    };
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

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


#region resolving dependencies
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

#endregion

#region resolving repositories

builder.Services.AddScoped<IAttendanceRepo, AttendanceRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<IDesignationRepo, DesignationRepo>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IFileRepo, FileRepo>();
builder.Services.AddScoped<IHolidayRepo, HolidayRepo>();
builder.Services.AddScoped<ILeavesRepo, LeavesRepo>();
builder.Services.AddScoped<ILeaveSettingRepo, LeaveSettingRepo>();
builder.Services.AddScoped<IOvertimeRepo, OvertimeRepo>();
builder.Services.AddScoped<ISchedulingRepo, SchedulingRepo>();
builder.Services.AddScoped<IShiftRepo, ShiftRepo>();
builder.Services.AddScoped<ITimesheetRepo, TimesheetRepo>();
builder.Services.AddScoped<INotesRepo, NotesRepo>();
builder.Services.AddScoped<IClientRepo, ClientRepo>();
builder.Services.AddScoped<ICustomPolicyRepo, CustomPolicyRepo>();
builder.Services.AddScoped<IProjectRepo, ProjectRepo>();
builder.Services.AddScoped<ITaskRepo, TaskRepo>();
builder.Services.AddScoped<ITaskBoardRepo, TaskBoardRepo>();
builder.Services.AddScoped<ITaskListRepo, TaskListRepo>();
builder.Services.AddScoped<ITicketRepo, TicketRepo>();
// builder.Services.AddScoped<IAnnualRepo, AnnualRepo>();
// builder.Services.AddScoped<ISickRepo, SickRepo>();
// builder.Services.AddScoped<IHospitalisationRepo, HospitalisationRepo>();
// builder.Services.AddScoped<IMaternityRepo, MaternityRepo>();
// builder.Services.AddScoped<IPaternityRepo, PaternityRepo>();
// builder.Services.AddScoped<ILopRepo, LopRepo>();
builder.Services.AddScoped<ITicketFollowersRepo, TicketFollowersRepo>();

#endregion

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue; //104857600; // 100MB
    options.MemoryBufferThreshold = int.MaxValue;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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

app.Run();
