﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = Aktitic.HrProject.DAL.Models.Task;
using Aktitic.HrProject.BL.Utilities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Aktitic.HrProject.DAL.Context;

public partial class HrSystemDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public static int TenantId { get; private set; }
    private readonly UserUtility? _userUtility;

    public HrSystemDbContext() { }
    public HrSystemDbContext(DbContextOptions<HrSystemDbContext> options, UserUtility? userUtility) : base(options)
    {
        _userUtility = userUtility;
        
        SetTenantId();           

        // if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator dbCreator)
        // {
        //     // Create Database 
        //     if (!dbCreator.CanConnect())
        //     {
        //         dbCreator.Create();
        //     }
        //
        //     // Create Tables
        //     if (!dbCreator.HasTables())
        //     {
        //         dbCreator.CreateTables();
        //     
        // }
        
    }

    private void SetTenantId()
    {
        if (_userUtility is not null)
        {
            TenantId = int.TryParse(_userUtility.GetCurrentCompany(), out var id) ? id : 0;
        }
    }

    #region Entities
    
    public virtual DbSet<ApplicationUser>? ApplicationUsers { get; set; }
    public virtual DbSet<Attendance>? Attendances { get; set; }

    public virtual DbSet<Department>? Departments { get; set; }

    public virtual DbSet<Designation>? Designations { get; set; }

    public virtual DbSet<Employee>? Employees { get; set; }

    public virtual DbSet<Document>? Documents { get; set; }
    public virtual DbSet<FileUsers>? FileUsers { get; set; }

    public virtual DbSet<Holiday>? Holidays { get; set; }

    public virtual DbSet<Leaves>? Leaves { get; set; }

    public virtual DbSet<Overtime>? Overtimes { get; set; }

    public virtual DbSet<Scheduling>? Schedulings { get; set; }

    public virtual DbSet<Shift>? Shifts { get; set; }

    public virtual DbSet<Client>? Clients { get; set; }

    public virtual DbSet<CustomPolicy>? CustomPolicies { get; set; }
    public virtual DbSet<Project>? Projects { get; set; }
    public virtual DbSet<Task>? Tasks { get; set; }
    public virtual DbSet<TaskList>? TaskLists { get; set; }
    public virtual DbSet<TaskBoard>? TaskBoards { get; set; }
    public virtual DbSet<Ticket>? Tickets { get; set; }
    public virtual DbSet<TicketFollowers>? TicketFollowers { get; set; }
    public virtual DbSet<TimeSheet>? Timesheets { get; set; }
    public virtual DbSet<Notes>? Notes { get; set; }
    public virtual DbSet<LeaveSettings>? LeaveSettings { get; set; }
    public virtual DbSet<Permission>? Permissions { get; set; }
    public virtual DbSet<EmployeeProjects>? EmployeeProjects { get; set; }
    public virtual DbSet<Message>? Messages { get; set; }
    public virtual DbSet<Estimate>? Estimates { get; set; }
    public virtual DbSet<Item>? Items { get; set; }
    public virtual DbSet<Invoice>? Invoices { get; set; }
    public virtual DbSet<Expenses>? Expenses { get; set; }
    public virtual DbSet<Payment>? Payments { get; set; }
    public virtual DbSet<Tax>? Taxes { get; set; }
    public virtual DbSet<ProvidentFunds>? ProvidentFunds { get; set; }
    public virtual DbSet<Category>? Categories { get; set; }
    public virtual DbSet<BudgetRevenue>? BudgetsRevenues { get; set; }
    public virtual DbSet<BudgetExpenses>? BudgetsExpenses { get; set; }
    public virtual DbSet<Budget>? Budgets { get; set; }
    public virtual DbSet<Revenue>? Revenues { get; set; }
    public virtual DbSet<ExpensesOfBudget>? ExpensesOfBudgets { get; set; }
    public virtual DbSet<Policies>? Polices { get; set; }
    public virtual DbSet<Salary>? Salaries { get; set; }
    public virtual DbSet<PayrollOvertime>? PayrollOvertimes { get; set; }
    public virtual DbSet<PayrollDeduction>? PayrollDeductions { get; set; }
    public virtual DbSet<PayrollAddition>? PayrollAdditions { get; set; }
    public virtual DbSet<PerformanceAppraisal>? PerformanceAppraisals { get; set; }
    public virtual DbSet<PerformanceIndicator>? PerformanceIndicators { get; set; }
    public virtual DbSet<Termination>? Terminations { get; set; }
    public virtual DbSet<Resignation>? Resignations { get; set; }
    public virtual DbSet<Promotion>? Promotions { get; set; }
    public virtual DbSet<Trainer>? Trainers { get; set; }
    public virtual DbSet<TrainingType>? TrainingTypes { get; set; }
    public virtual DbSet<TrainingList>? TrainingLists { get; set; }
    public virtual DbSet<GoalType>? GoalTypes { get; set; }
    public virtual DbSet<GoalList>? GoalLists { get; set; }
    public virtual DbSet<Event>? Events { get; set; }
    public virtual DbSet<Contact>? Contacts { get; set; }
    public virtual DbSet<Contract>? Contracts { get; set; }
    public virtual DbSet<Company>? Companies { get; set; }   
    public virtual DbSet<License>? Licenses { get; set; }   
    public virtual DbSet<Notification>? Notifications { get; set; }   
    public virtual DbSet<ReceivedNotification>? ReceivedNotifications { get; set; }   
    public virtual DbSet<ChatGroup>? ChatGroups { get; set; }   
    public virtual DbSet<ChatGroupUser>? ChatGroupUsers { get; set; }   
    public virtual DbSet<Asset>? Assets { get; set; }   
    public virtual DbSet<Job>? Jobs { get; set; }   
    public virtual DbSet<Shortlist>? Shortlists { get; set; }   
    public virtual DbSet<InterviewQuestion>? InterviewQuestions { get; set; }   
    public virtual DbSet<OfferApproval>? OfferApprovals { get; set; }   
    public virtual DbSet<Experience>? Experiences { get; set; }   
    public virtual DbSet<Candidate>? Candidates { get; set; }   
    public virtual DbSet<ScheduleTiming>? ScheduleTimings { get; set; }   
    public virtual DbSet<AptitudeResult>? AptitudeResults { get; set; }   
    public virtual DbSet<JobApplicant>? JobApplicants { get; set; }   
    public virtual DbSet<AuditLog>? AuditLogs { get; set; }   
    public virtual DbSet<AppPages>? AppPages { get; set; }   
    public virtual DbSet<LogAction>? LogActions { get; set; }   
    public virtual DbSet<Email>? Emails { get; set; }   
    public virtual DbSet<MailAttachment>? Attachments { get; set; }   
    public virtual DbSet<DocumentFile>? DocumentFiles { get; set; }   
    public virtual DbSet<Revisor>? Revisors { get; set; }   
    public virtual DbSet<AppModule>? AppModules { get; set; }   
    public virtual DbSet<AppSubModule>? AppSubModules { get; set; }   
    public virtual DbSet<CompanyModule>? CompanyModules { get; set; }   
    public virtual DbSet<CompanyRole>? CompanyRoles { get; set; }   
    public virtual DbSet<RolePermissions>? RolePermissions { get; set; }   
    

    #endregion


     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
         
         SetTenantId();
         // var tenantConnectionString = _tenantServices.GetConnectionString();
         //
         // if (!string.IsNullOrEmpty(tenantConnectionString))
         // {
         //     var dbProvider = _tenantServices.GetDatabaseProvider();
         //     
         //     if (dbProvider?.ToLower() == "mssql")
         //     {
         //         optionsBuilder.UseSqlServer(tenantConnectionString);
         //     }
         //     
         // 
     }

     

     public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
     {
         foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>()
                      .Where(e => e.State == EntityState.Added && GetCurrentTenantId() != 0))
         {
             entry.Entity.TenantId ??= GetCurrentTenantId();
         }
         
         var pageNames = new Dictionary<string, int>
         {
             { "/api/chat/create", 1 },
             { "/api/events", 2 },
             { "/api/contacts", 3 },
             { "/api/files", 4 },
             { "/api/employees", 5 },
             { "/api/contracts", 6 },
             { "/api/holiday", 7 },
             { "/api/leaves", 8 },
             { "/api/leavesettings", 9 },
             { "/api/attendances", 10 },
             { "/api/departments", 11 },
             { "/api/designations", 12 },
             { "/api/timesheets", 13 },
             { "/api/scheduling", 14 },
             { "/api/shifts", 15 },
             { "/api/overtime", 16 },
             { "/api/clients", 17 },
             { "/api/projects", 18 },
             { "/api/tasks", 19 },
             { "/api/taskboard", 20 },
             { "/api/ticket", 21 },
             { "/api/estimate", 22 },
             { "/api/invoice", 23 },
             { "/api/payments", 24 },
             { "/api/expenses", 25 },
             { "/api/providentfunds", 26 },
             { "/api/taxes", 27 },
             { "/api/categories", 28 },
             { "/api/budgets", 29 },
             { "/api/budgetsexpenses", 30 },
             { "/api/budgetsrevenues", 31 },
             { "/api/salaries", 32 },
             { "/api/payrollovertime", 33 },
             { "/api/payrolldeduction", 34 },
             { "/api/payrolladdition", 35 },
             { "/api/policies", 36 },
             { "/api/attendances/gettodayemployeeattendance", 37 },
             { "/api/performanceindicators", 38 },
             { "/api/performanceappraisals", 39 },
             { "/api/goallists", 40 },
             { "/api/goaltypes", 41 },
             { "/api/traininglists", 42 },
             { "/api/trainers", 43 },
             { "/api/trainingtypes", 44 },
             { "/api/promotions", 45 },
             { "/api/resignations", 46 },
             { "/api/terminations", 47 },
             { "/api/assets", 48 },
             { "/api/jobs", 49 },
             { "/api/jobapplicants", 50 },
             { "/api/shortlists", 51 },
             { "/api/interviewquestions", 52 },
             { "/api/offerapprovals", 53 },
             { "/api/experiences", 54 },
             { "/api/candidaties", 55 },
             { "/api/secheduletimings", 56 },
             { "/api/aptituderesults", 57 },
             { "/api/users", 58 },
             { "/api/companies", 59 },
             { "/api/licenses", 60 },
             { "/api/notifications", 61 }
             // Add more mappings as needed
         };

         var modifiedEntities = ChangeTracker
             .Entries<IAuditable>()
             .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added || e.Entity.IsDeleted);

         foreach (var entry in modifiedEntities.ToList())
         {
             var pageName = pageNames.FirstOrDefault(x => _userUtility.GetCurrentPage()
                 .Contains(x.Key,StringComparison.OrdinalIgnoreCase)).Value;
             var actionName = entry.State.ToString();
             var auditLog = new AuditLog()
             {
                 EntityName = entry.Entity.GetType().Name,
                 UserId = _userUtility?.GetUserId(),
                 Action = LogActions?.FirstOrDefault(x => x.Name == actionName),
                 TimeStamp = DateTime.Now,
                 Changes = GetChanges(entry),
                 IpAddress = _userUtility.GetIpAddress(),
                 TenantId = GetCurrentTenantId(), 
                 // AppPages = AppPages?.FirstOrDefault(x=>x.Id == pageName),
                                                    
                 // id of the added item
                 ModifiedRecords = new List<ModifiedRecord>()
                 {
                     new()
                     {
                         RecordId = GetRecordId(entry),
                         PermenantlyDeleted = false,
                         PermenantlyDeletedBy = null
                     }
                 }
             };



             AuditLogs?.Add(auditLog);

             if ((entry.State == EntityState.Added))
             {
                 entry.Entity.CreatedAt = DateTime.Now;
                 entry.Entity.CreatedBy = _userUtility.GetUserName();
             }

             if ((entry.State == EntityState.Modified))
             {
                 entry.Entity.UpdatedAt = DateTime.Now;
                 entry.Entity.UpdatedBy = _userUtility?.GetUserName();
             }

             if (entry.Entity.IsDeleted)
             {
                 entry.Entity.DeletedAt = DateTime.Now;
                 entry.Entity.DeletedBy = _userUtility?.GetUserName();
             }
         }
         
         
         var save = await base.SaveChangesAsync(cancellationToken);
         UpdatePendingAuditLogs();
         await base.SaveChangesAsync(cancellationToken);
         return save;
     }

     
    private string? GetChanges(EntityEntry<IAuditable> entry)
    {
        var changes = new List<string>();
        foreach (var property in entry.Properties.ToList())
        {
            var propertyName = property.Metadata.Name;
            var currentValue = property.CurrentValue;
            var originalValue = property.OriginalValue;
            if (currentValue != null && originalValue != null)
            {
                if (!currentValue.Equals(originalValue))
                {
                    changes.Add($"{propertyName}: {originalValue} -> {currentValue}");
                }
            }
        }

        return JsonSerializer.Serialize(changes);
    }
   

    private string GetRecordId(EntityEntry<IAuditable> entry)
    {
        // Fallback to looking for a property named "Id" if no primary key is found
        var idProperty = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey()) ?? entry.Properties.FirstOrDefault(p => p.Metadata.Name == "Id");

        if (idProperty != null)
        {
            if (entry.State == EntityState.Added)
            {
                // For newly added entities, we need to handle the case where the ID hasn't been generated yet
                if (idProperty.CurrentValue == null || 
                    idProperty.CurrentValue is <= 0 )
                {
                    // The ID hasn't been generated yet
                    return "Pending";
                }
            }

            // For all other cases, return the current value of the ID property
            return idProperty.CurrentValue?.ToString() ?? "Unknown";
        }

        // If we couldn't find an ID property at all
        return "Unknown";
    }

    private void UpdatePendingAuditLogs()
    {
        if (AuditLogs != null)
        {
            var pendingAuditLogs = AuditLogs.Where(log => log.ModifiedRecords != null && log.ModifiedRecords.Any(r => r.RecordId == "Pending")).ToList();
            foreach (var auditLog in pendingAuditLogs)
            {
                var entity = ChangeTracker.Entries<IAuditable>()
                    .FirstOrDefault(e => e.Entity.GetType().Name == auditLog.EntityName);
        
                if (entity != null)
                {
                    var newRecordId = GetRecordId(entity);
                    if (newRecordId != "Pending" && newRecordId != "Unknown")
                    {
                        if (auditLog.ModifiedRecords != null) auditLog.ModifiedRecords.First().RecordId = newRecordId;
                    }
                }
            }
        }
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedData(modelBuilder);
        // get non deleted records
        // Apply global query filters for all entities that implements IBaseEntity
        // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        // {
        //    
        // }
        // var tenantId = GetCurrentTenantId(); // Call outside the filter expression
        //
        //  foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //  {
        //      // LambdaExpression tenantIdFilter = null;
        //      // Apply IsDeleted filter for IAuditable entities
        //      // if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
        //      // {
        //      //     var isDeletedFilter = CreateIsDeletedFilter(entityType.ClrType,tenantId);
        //      //     modelBuilder.Entity(entityType.ClrType).HasQueryFilter(isDeletedFilter);
        //      // }
        //
        //      // var tenantId = GetCurrentTenantId();  // هات الـ TenantId الحالي
        //      if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
        //      {
        //          modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateIsDeletedFilter(entityType.ClrType));
        //          // modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateTenantIdFilter(entityType.ClrType, tenantId));
        //
        //      }
        //
        //      if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
        //      {
        //      }
        //      // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //      // {
        //          // إضافة فلتر الـ IsDeleted إذا كانت الكلاس تطبق الـ IAuditable
        //          // if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
        //          // {
        //          //     modelBuilder.Entity(entityType.ClrType)
        //          //         .HasQueryFilter(e => EF.Property<bool>(e, "IsDeleted") == false);
        //          // }
        //          //
        //          // // إضافة فلتر الـ TenantId إذا كانت الكلاس تطبق الـ ITenantEntity
        //          // if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
        //          // {
        //          //     modelBuilder.Entity(entityType.ClrType)
        //          //         .HasQueryFilter(e => EF.Property<int>(e, "TenantId") == tenantId);
        //          // }
        //          // }
        //          // // Apply TenantId filter for IBaseEntity entities
        //          // if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
        //          // { 
        //          //     tenantIdFilter = CreateTenantIdFilter(entityType.ClrType, tenantId);
        //          //     // modelBuilder.Entity(entityType.ClrType).HasQueryFilter(tenantIdFilter);
        //          // }
        //          //
        //          //
        //          // if (isDeletedFilter != null && tenantIdFilter != null)
        //          // {
        //          //     var filter = CombineFilters(isDeletedFilter, tenantIdFilter);
        //          //     if (filter != null)
        //          //     {
        //          //         modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
        //          //     }
        //          // }
        //  }
        //  
        
        var tenantId = GetCurrentTenantId(); // Call outside the filter expression

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            LambdaExpression? isDeletedFilter = null;
            LambdaExpression? tenantIdFilter = null;

            // Apply IsDeleted filter for IAuditable entities
            if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
            {
                isDeletedFilter = CreateIsDeletedFilter(entityType.ClrType);
            }

            // Apply TenantId filter for IBaseEntity entities
            if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                tenantIdFilter = CreateTenantIdFilter(entityType.ClrType, tenantId);
            }

            // Combine both filters
            // if (isDeletedFilter != null && tenantIdFilter != null)
            // {
            //     var combinedFilter = CombineFilters(isDeletedFilter, tenantIdFilter);
            //     modelBuilder.Entity(entityType.ClrType).HasQueryFilter(combinedFilter);
            // }
            // else if (isDeletedFilter != null)
            // {
            //     modelBuilder.Entity(entityType.ClrType).HasQueryFilter(isDeletedFilter);
            // }
            // else if (tenantIdFilter != null)
            // {
            //     modelBuilder.Entity(entityType.ClrType).HasQueryFilter(tenantIdFilter);
            // }
        }
        
        
        // // Create parameter expression
        // var parameter = Expression.Parameter(typeof(YourEntity), "x");
        //
        // // Create filter expressions
        // var isDeletedFilter = Expression.Lambda(
        //     Expression.NotEqual(
        //         Expression.Property(parameter, nameof(YourEntity.IsDeleted)),
        //         Expression.Constant(true)
        //     ), parameter);
        //
        // var tenantIdFilter = Expression.Lambda(
        //     Expression.Equal(
        //         Expression.Property(parameter, nameof(YourEntity.TenantID)),
        //         Expression.Constant(1)
        //     ), parameter);
        //
        // // Combine filters
        // var combinedFilter = CombineFilters(isDeletedFilter, tenantIdFilter);
        //
        // Console.WriteLine(combinedFilter);
        //////////
        //
        //  Apply tenant filter
        //  foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //   {
        //       LambdaExpression queryFilter = null;
        //  
        //       // Apply IsDeleted filter for IAuditable
        //  
        //       if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
        //       {
        //           var method = _setIsDeletedFilterMethod?.MakeGenericMethod(entityType.ClrType);
        //           if (method?.Invoke(this, [modelBuilder]) is LambdaExpression isDeletedFilter)
        //           {
        //               queryFilter = isDeletedFilter;
        //           }
        //           else
        //           {
        //               Console.WriteLine($"IsDeleted filter is null for entity type {entityType.ClrType.Name}");
        //           }
        //  //      }
        //  //
         //      // Apply TenantId filter for IBaseEntity
         //      if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
         //      {
         //          var method = _setTenantIdFilterMethod?.MakeGenericMethod(entityType.ClrType);
         //          if (method?.Invoke(this, [modelBuilder]) is LambdaExpression tenantFilter)
         //          {
         //              queryFilter =  CombineFilters(queryFilter, tenantFilter);
         //          }
         //          else
         //          {
         //              Console.WriteLine($"Tenant filter is null for entity type {entityType.ClrType.Name}");
         //          }
         //      }
         //
         //          // Apply the combined filter
         //          if (queryFilter != null)
         //          {
         //              modelBuilder.Entity(entityType.ClrType).HasQueryFilter(queryFilter);
         //          }
         //  }
         // //  
        
         // add the configuration 
        
         modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrSystemDbContext).Assembly);   
      
        
        
         // modelBuilder.Entity<Attendance>().HasQueryFilter(e => e.IsDeleted == false);
        
        
         modelBuilder.Entity<ChatGroupUser>()
             .HasKey(cgu => new { cgu.ChatGroupId, cgu.UserId });

         modelBuilder.Entity<ChatGroupUser>()
             .HasOne(cgu => cgu.ChatGroup)
             .WithMany(cg => cg.ChatGroupUsers)
             .HasForeignKey(cgu => cgu.ChatGroupId);

         modelBuilder.Entity<ChatGroupUser>()
             .HasOne(cgu => cgu.User)
             .WithMany(u => u.ChatGroupUsers)
             .HasForeignKey(cgu => cgu.UserId);
        
      
        
        
         modelBuilder.Entity<Company>()
             .HasOne(c => c.Manager)
             .WithOne(u => u.ManagedCompany)
             .HasForeignKey<Company>(c => c.ManagerId);

         // One-to-Many relationship between Company and Users
         modelBuilder.Entity<Company>()
             .HasMany(c => c.Users)
             .WithOne(u => u.Company)
             .HasForeignKey(u => u.TenantId);
        
        
        
        
        
         modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
         {
             entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
         });
        
         modelBuilder.Entity<IdentityUserRole<int>>(entity =>
         {
             entity.HasKey(e => new { e.UserId, e.RoleId });
         });
        
         modelBuilder.Entity<IdentityUserToken<int>>(entity =>
         {
             entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
         });
        
         modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
         {
             entity.HasKey(e => new { e.Id });
         });
        
         modelBuilder.Entity<ApplicationUser>(entity =>
         {
             entity.Property(e => e.Id).ValueGeneratedOnAdd();
         });
        
        
        
     
         // modelBuilder.Entity<Permission>()
             // .HasOne(p => p.ApplicationUser)
             // .WithMany(u => u.Permissions)
             // .HasForeignKey(p => p.UserId)
             // .OnDelete(DeleteBehavior.Cascade);

         OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    
    
    private static LambdaExpression? CreateIsDeletedFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
        var comparison = Expression.Equal(isDeletedProperty, Expression.Constant(false));
        return Expression.Lambda(comparison, parameter);
    }

    private static LambdaExpression CreateTenantIdFilter(Type entityType, int tenantId)
    {
        var parameter = Expression.Parameter(entityType, "e");

        // Access the TenantId property (which is nullable)
        var tenantIdProperty = Expression.Property(parameter, "TenantId");

        // Cast the nullable TenantId to a non-nullable int
        var tenantIdConverted = Expression.Convert(tenantIdProperty, typeof(int));

        // Define the value to compare with (the passed non-nullable tenantId)
        var tenantIdConstant = Expression.Constant(tenantId, typeof(int));

        // Create the comparison (TenantId == tenantId)
        var comparison = Expression.Equal(tenantIdConverted, tenantIdConstant);

        // Return the lambda expression
        return Expression.Lambda(comparison, parameter);
    }

    private static void SetTenantIdFilter<T>(ModelBuilder builder,int tenantId) where T : BaseEntity
    {
        builder.Entity<T>().HasQueryFilter(e => e.TenantId == tenantId);
    }

    private static int GetCurrentTenantId()
    {
        return TenantId;
    }

    // private void SetTenantIdFilter<T>(ModelBuilder modelBuilder) where T : class, IBaseEntity
    // {
    //     modelBuilder.Entity<T>().HasQueryFilter(e => e.TenantId == GetCurrentTenantId());
    // }
    //
    // private readonly MethodInfo? _setTenantIdFilterMethod = typeof(HrSystemDbContext)
    //     .GetMethod(nameof(SetTenantIdFilter), BindingFlags.NonPublic | BindingFlags.Instance);
    //
    //  private void SetIsDeletedFilter<T>(ModelBuilder modelBuilder) where T : class, IAuditable
    // {
    //     modelBuilder.Entity<T>().HasQueryFilter(e => e.IsDeleted == false);
    // }
    //
    // private readonly MethodInfo? _setIsDeletedFilterMethod = typeof(HrSystemDbContext)
    //     .GetMethod(nameof(SetIsDeletedFilter), BindingFlags.NonPublic | BindingFlags.Instance);

    

    private static LambdaExpression? CombineFilters(LambdaExpression firstFilter, LambdaExpression secondFilter)
    {
        var parameter = firstFilter.Parameters[0]; // Assuming both filters use the same parameter

        // Combine the bodies of the two expressions using AndAlso (logical AND)
        var combinedBody = Expression.AndAlso(firstFilter.Body, secondFilter.Body);

        // Return a new lambda expression with the combined body
        return Expression.Lambda(combinedBody, parameter);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var modules = new List<AppModule>();
        var subModules = new List<AppSubModule>();
        var pages = new List<AppPages>();

        int moduleId = 1, subModuleId = 1, pageId = 1;

        // Data from JSON and Arabic values
        var data = new[]
        {
            new
            {
                Module = "Main",
                SubModules = new[]
                {
                    new
                    {
                        Name = "Dashboard",
                        Pages = new[]
                        {
                            new { Name = "Admin Dashboard", ArabicName = "لوحة التحكم الإدارية" },
                            new { Name = "Employee Dashboard", ArabicName = "لوحة تحكم الموظفين" }
                        }
                    },
                    new
                    {
                        Name = "App",
                        Pages = new[]
                        {
                            new { Name = "Chat", ArabicName = "محادثة" },
                            new { Name = "Calendar", ArabicName = "تقويم" },
                            new { Name = "Contacts", ArabicName = "جهات الاتصال" },
                            new { Name = "Email", ArabicName = "البريد الإلكتروني" },
                            new { Name = "Documents Manager", ArabicName = "مدير الملفات" },
                            new { Name = "Documents Workflows", ArabicName = "سير عمل الملفات" },
                            new { Name = "Documents Details View", ArabicName = "عرض تفاصيل الملفات" }
                        }
                    }
                }
            },
            new
            {
                Module = "Employees",
                SubModules = new[]
                {
                    new
                    {
                        Name = "Employees",
                        Pages = new[]
                        {
                            new { Name = "All Employees", ArabicName = "كل الموظفين" },
                            new { Name = "Contracts", ArabicName = "عقود" },
                            new { Name = "Holidays", ArabicName = "العطل" },
                            new { Name = "Leaves (Admin)", ArabicName = "الإجازات (أدمن)" },
                            new { Name = "Leaves (Employee)", ArabicName = "الإجازات (موظف)" },
                            new { Name = "Leave Settings", ArabicName = "إعدادات الإجازات" },
                            new { Name = "Attendance (Admin)", ArabicName = "الحضور (أدمن)" },
                            new { Name = "Attendance (Employee)", ArabicName = "الحضور (موظف)" },
                            new { Name = "Departments", ArabicName = "الأقسام" },
                            new { Name = "Designations", ArabicName = "التسميات" },
                            new { Name = "Shift & Schedule", ArabicName = "جدول المناوبة" },
                            new { Name = "Overtime", ArabicName = "الوقت الإضافي" }
                        }
                    },
                    new
                    {
                        Name = "Clients",
                        Pages = new[]
                        {
                            new { Name = "Clients", ArabicName = "العملاء" }
                        }
                    },
                    new
                    {
                        Name = "Projects",
                        Pages = new[]
                        {
                            new { Name = "Projects", ArabicName = "المشاريع" },
                            new { Name = "Tasks", ArabicName = "المهام" },
                            new { Name = "Task Board", ArabicName = "لوحة المهام" }
                        }
                    },
                    new
                    {
                        Name = "Leads",
                        Pages = new[]
                        {
                            new { Name = "Leads", ArabicName = "العملاء المحتملين" }
                        }
                    },
                    new
                    {
                        Name = "Tickets",
                        Pages = new[]
                        {
                            new { Name = "Tickets", ArabicName = "التذاكر" }
                        }
                    }
                }
            },
            new
            {
                Module = "HR",
                SubModules = new[]
                {
                    new
                    {
                        Name = "Sales",
                        Pages = new[]
                        {
                            new { Name = "Estimate", ArabicName = "تقدير" },
                            new { Name = "Invoices", ArabicName = "الفواتير" },
                            new { Name = "Payments", ArabicName = "المدفوعات" },
                            new { Name = "Expenses", ArabicName = "النفقات" },
                            new { Name = "Provident Fund", ArabicName = "صندوق الادخار" },
                            new { Name = "Taxes", ArabicName = "الضرائب" }
                        }
                    },
                    new
                    {
                        Name = "Accounting",
                        Pages = new[]
                        {
                            new { Name = "Categories", ArabicName = "الفئات" },
                            new { Name = "Budgets", ArabicName = "الميزانيات" },
                            new { Name = "Budgets Expenses", ArabicName = "نفقات الميزانيات" },
                            new { Name = "Budgets Revenues", ArabicName = "إيرادات الميزانيات" }
                        }
                    },
                    new
                    {
                        Name = "Payroll",
                        Pages = new[]
                        {
                            new { Name = "Employee Salary", ArabicName = "رواتب الموظفين" },
                            new { Name = "Payroll Items", ArabicName = "عناصر الرواتب" }
                        }
                    },
                    new
                    {
                        Name = "Policies",
                        Pages = new[]
                        {
                            new { Name = "Policies", ArabicName = "السياسات" }
                        }
                    },
                    new
                    {
                        Name = "Reports",
                        Pages = new[]
                        {
                            new { Name = "Expense Report", ArabicName = "تقرير النفقات" },
                            new { Name = "Invoice Report", ArabicName = "تقرير الفواتير" },
                            new { Name = "Payments Report", ArabicName = "تقرير المدفوعات" },
                            new { Name = "Project Report", ArabicName = "تقرير المشروع" },
                            new { Name = "Task Report", ArabicName = "تقرير المهام" },
                            new { Name = "User Report", ArabicName = "تقرير المستخدم" },
                            new { Name = "Employee Report", ArabicName = "تقرير الموظفين" },
                            new { Name = "Payslip Report", ArabicName = "تقرير قسيمة الراتب" },
                            new { Name = "Attendance Report", ArabicName = "تقرير الحضور" },
                            new { Name = "Leave Report", ArabicName = "تقرير الإجازات" },
                            new { Name = "Daily Report", ArabicName = "تقرير يومي" }
                        }
                    }
                }
            },
            new
            {
                Module = "Performance",
                SubModules = new[]
                {
                    new
                    {
                        Name = "Performance",
                        Pages = new[]
                        {
                            new { Name = "Performance indicator", ArabicName = "مؤشر الأداء" },
                            new { Name = "Performance Review", ArabicName = "مراجعة الأداء" },
                            new { Name = "Performance Appraisal", ArabicName = "تقييم الأداء" }
                        }
                    },
                    new
                    {
                        Name = "Goals",
                        Pages = new[]
                        {
                            new { Name = "Goal List", ArabicName = "قائمة الأهداف" },
                            new { Name = "Goal Type", ArabicName = "نوع الهدف" }
                        }
                    },
                    new
                    {
                        Name = "Training",
                        Pages = new[]
                        {
                            new { Name = "Training List", ArabicName = "قائمة التدريب" },
                            new { Name = "Trainers", ArabicName = "المدربون" },
                            new { Name = "Training Type", ArabicName = "نوع التدريب" }
                        }
                    },
                    new
                    {
                        Name = "Promotion",
                        Pages = new[]
                        {
                            new { Name = "Promotion", ArabicName = "ترقية" }
                        }
                    },
                    new
                    {
                        Name = "Resignation",
                        Pages = new[]
                        {
                            new { Name = "Resignation", ArabicName = "استقالة" }
                        }
                    },
                    new
                    {
                        Name = "Termination",
                        Pages = new[]
                        {
                            new { Name = "Termination", ArabicName = "فسخ العقد" }
                        }
                    }
                }
            },
            new
            {
                Module = "Administration",
                SubModules = new[]
                {
                    new
                    {
                        Name = "Assets",
                        Pages = new[]
                        {
                            new { Name = "Assets", ArabicName = "الأصول" }
                        }
                    },
                    new
                    {
                        Name = "Jobs",
                        Pages = new[]
                        {
                            new { Name = "User Dashboard", ArabicName = "لوحة تحكم المستخدم" },
                            new { Name = "Jobs Dashboard", ArabicName = "لوحة تحكم الوظائف" },
                            new { Name = "Manage Jobs", ArabicName = "إدارة الوظائف" },
                            new { Name = "Manage Resumes", ArabicName = "إدارة السير الذاتية" },
                            new { Name = "Shortlist Candidates", ArabicName = "قائمة المرشحين المختصرة" },
                            new { Name = "Interview Questions", ArabicName = "أسئلة المقابلة" },
                            new { Name = "Offer Approvals", ArabicName = "الموافقات على العروض" },
                            new { Name = "Experience Level", ArabicName = "مستوى الخبرة" },
                            new { Name = "Candidates List", ArabicName = "قائمة المرشحين" },
                            new { Name = "Schedule Timing", ArabicName = "توقيت الجدول الزمني" },
                            new { Name = "Aptitude Results", ArabicName = "نتائج الكفاءة" },
                            new { Name = "Applied Candidates", ArabicName = "المرشحون المتقدمون" }
                        }
                    },
                    new
                    {
                        Name = "Knowledgebase",
                        Pages = new[]
                        {
                            new { Name = "Knowledgebase", ArabicName = "قاعدة المعرفة" }
                        }
                    },
                    new
                    {
                        Name = "Activities",
                        Pages = new[]
                        {
                            new { Name = "Activities", ArabicName = "الأنشطة" }
                        }
                    },
                    new
                    {
                        Name = "Users",
                        Pages = new[]
                        {
                            new { Name = "Users", ArabicName = "المستخدمون" }
                        }
                    },
                    new
                    {
                        Name = "Companies",
                        Pages = new[]
                        {
                            new { Name = "Companies", ArabicName = "الشركات" }
                        }
                    },
                    new
                    {
                        Name = "Licenses",
                        Pages = new[]
                        {
                            new { Name = "Licenses", ArabicName = "التراخيص" }
                        }
                    },
                    new
                    {
                        Name = "Notifications",
                        Pages = new[]
                        {
                            new { Name = "Notifications", ArabicName = "الإشعارات" }
                        }
                    },
                    new
                    {
                        Name = "User Activities",
                        Pages = new[]
                        {
                            new { Name = "User Activities", ArabicName = "أنشطة المستخدم" }
                        }
                    },
                    new
                    {
                        Name = "Trash",
                        Pages = new[]
                        {
                            new { Name = "Trash", ArabicName = "المهملات" }
                        }
                    },
                    new
                    {
                        Name = "Settings",
                        Pages = new[]
                        {
                            new { Name = "Settings", ArabicName = "الإعدادات" }
                        }
                    }
                }
            },
            new
            {
                Module = "Pages",
                SubModules = new[]
                {
                    new
                    {
                        Name = "Profile",
                        Pages = new[]
                        {
                            new { Name = "Employee Profile", ArabicName = "ملف الموظف" }
                        }
                    }
                }
            }
        };

        foreach (var moduleData in data)
        {
            var module = new AppModule
            {
                Id = moduleId,
                Name = moduleData.Module,
            };
            modules.Add(module);

            foreach (var subModuleData in moduleData.SubModules)
            {
                var subModule = new AppSubModule
                {
                    Id = subModuleId,
                    Name = subModuleData.Name,
                    AppModuleId = moduleId
                };
                subModules.Add(subModule);

                foreach (var pageData in subModuleData.Pages)
                {
                    var page = new AppPages
                    {
                        // Id = pageId,
                        Name = pageData.Name,
                        Code = pageData.Name.Replace(" ", "").Replace("(", "").Replace(")", ""),
                        ArabicName = pageData.ArabicName,
                        AppSubModuleId = subModuleId
                    };
                    pages.Add(page);
                    pageId++;
                }

                subModuleId++;
            }

            moduleId++;
        }

        // Seed Data
        modelBuilder.Entity<AppModule>().HasData(modules);
        modelBuilder.Entity<AppSubModule>().HasData(subModules);
        modelBuilder.Entity<AppPages>().HasData(pages);
    }
}

