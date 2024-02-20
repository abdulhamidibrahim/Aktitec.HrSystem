using System.Text;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

//add connection string
builder.Services.AddDbContext<HrManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HrManagementDbConnection"),
        option => option.CommandTimeout(300)));



builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<HrManagementDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddIdentityCore<Employee>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<HrManagementDbContext>()
    .AddDefaultTokenProviders();

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



#region resolving dependencies
builder.Services.AddScoped<IAttendanceManager, AttendanceManager>();
builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
builder.Services.AddScoped<IDesignationManager, DesignationManager>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
builder.Services.AddScoped<IHolidayManager, HolidayManager>();
builder.Services.AddScoped<ILeavesManager, LeavesManager>();
builder.Services.AddScoped<IOvertimeManager, OvertimeManager>();
builder.Services.AddScoped<ISchedulingManager, SchedulingManager>();
builder.Services.AddScoped<IShiftManager, ShiftManager>();
builder.Services.AddScoped<ITimesheetManager, TimesheetManager>();
builder.Services.AddScoped<IFileRepo, FileRepo>();

#endregion

#region resolving repositories

builder.Services.AddScoped<IAttendanceRepo, AttendanceRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<IDesignationRepo, DesignationRepo>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IFileRepo, FileRepo>();
builder.Services.AddScoped<IHolidayRepo, HolidayRepo>();
builder.Services.AddScoped<ILeavesRepo, LeavesRepo>();
builder.Services.AddScoped<IOvertimeRepo, OvertimeRepo>();
builder.Services.AddScoped<ISchedulingRepo, SchedulingRepo>();
builder.Services.AddScoped<IShiftRepo, ShiftRepo>();
builder.Services.AddScoped<ITimesheetRepo, TimesheetRepo>();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
