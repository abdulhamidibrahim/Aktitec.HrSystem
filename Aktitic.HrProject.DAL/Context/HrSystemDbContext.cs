using System.Linq.Expressions;
using System.Reflection;
using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = Aktitic.HrProject.DAL.Models.Task;
using Aktitic.HrProject.BL.Utilities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;


namespace Aktitic.HrProject.DAL.Context;

public partial class HrSystemDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    private static int? TenantId { get; set; }
    private readonly UserUtility? _userUtility;
    
    public HrSystemDbContext(DbContextOptions<HrSystemDbContext> options, UserUtility? userUtility) : base(options)
    {
        _userUtility = userUtility;
        if (userUtility?.GetCurrentCompany() == "Company")
            TenantId = 0;
        else TenantId = string.IsNullOrEmpty(userUtility?.GetCurrentCompany()) ? 0
            : int.Parse(userUtility.GetCurrentCompany());
        
        
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

    #region Entities
    
    public virtual DbSet<ApplicationUser>? ApplicationUsers { get; set; }
    public virtual DbSet<Attendance>? Attendances { get; set; }

    public virtual DbSet<Department>? Departments { get; set; }

    public virtual DbSet<Designation>? Designations { get; set; }

    public virtual DbSet<Employee>? Employees { get; set; }

    public virtual DbSet<File>? Files { get; set; }
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

    #endregion


    //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //  {
    //
    //      var tenantConnectionString = _tenantServices.GetConnectionString();
    //      
    //      if (!string.IsNullOrEmpty(tenantConnectionString))
    //      {
    //          var dbProvider = _tenantServices.GetDatabaseProvider();
    //          
    //          if (dbProvider?.ToLower() == "mssql")
    //          {
    //              optionsBuilder.UseSqlServer(tenantConnectionString);
    //          }
    //          
    //      }
    // }
   
    
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>()
                     .Where(e=>e.State == EntityState.Added && GetCurrentTenantId()!=0))
        {
            entry.Entity.TenantId = GetCurrentTenantId();
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // get non deleted records
        // Apply global query filters for all entities that derive from BaseEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateIsDeletedFilter(entityType.ClrType));
            }
        }
        
        
        // Apply tenant filter
       foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = SetTenantIdFilterMethod.MakeGenericMethod(entityType.ClrType);
                method.Invoke(this, new object[] { modelBuilder });
            }
        }
        
        
        

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



        modelBuilder.Entity<License>()
            .HasQueryFilter(e => e.TenantId == TenantId);
        
        // modelBuilder.Entity<ApplicationUser>()
        //     .HasQueryFilter(e => e.TenantId == TenantId);
        
        
        
        // application user 
        // modelBuilder.Entity<ApplicationUser>(entity =>
        // {
        //     entity.HasOne(x => x.ManagedCompany)
        //         .WithOne(x => x.Manager)
        //         .HasForeignKey<Company>(x => x.ManagerId);
        //     
        //     entity.HasOne<Company>(x => x.Company)
        //         .WithMany(x => x.Users)
        //         .OnDelete(DeleteBehavior.Cascade);
        // });
        
        
        modelBuilder.Entity<Company>()
            .HasOne(c => c.Manager)
            .WithOne(u => u.ManagedCompany)
            .HasForeignKey<Company>(c => c.ManagerId);

        // One-to-Many relationship between Company and Users
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Users)
            .WithOne(u => u.Company)
            .HasForeignKey(u => u.TenantId);
        
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.ToTable("Attendance", "employee");

            entity.Property(e => e.Break).HasColumnName("break");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            // entity.Property(e => e.OvertimeId).HasColumnName("overtime_id");
            entity.Property(e => e.Production).HasColumnName("production");
            entity.Property(e => e.PunchIn)
                .HasColumnType("datetime")
                .HasColumnName("punch_in");
            entity.Property(e => e.PunchOut)
                .HasColumnType("datetime")
                .HasColumnName("punch_out");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Attendance_Employee");

            // entity.HasOne(d => d.Overtime).WithMany(p => p.Attendances)
            //     .HasForeignKey(d => d.OvertimeId)
            //     .HasConstraintName("FK_Attendance_Overtimes");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department", "employee");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.ToTable("Designation", "employee");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.Department)
                .WithMany(p => p.Designations)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Designation_Department");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee", "employee");

            // entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.ImgId).HasColumnName("img_id");
            entity.Property(e => e.JobPosition)
                .HasMaxLength(50)
                .HasColumnName("job_position");
            entity.Property(e => e.JoiningDate).HasColumnName("joining_date");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.YearsOfExperience).HasColumnName("years_of_experience");
        
            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Employee_Department_1");
        
            //project relationship
            // entity.HasOne(d => d.Project).WithMany(p => p.Employees)
            //     .HasForeignKey(d => d.ProjectId)
            //     .HasConstraintName("FK_Employee_Project");
            
            // entity.HasOne(d => d.Manager).WithMany(p => p.Employees)    
            //     .HasForeignKey(d => d.ManagerId)
            //     .HasConstraintName("FK_Employee_Employee");
            // entity.HasOne(d => d.Img).WithMany(p => p.Employees)
            //     .HasForeignKey(d => d.ImgId)
            //     .HasConstraintName("FK_Employee_File_1");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.ToTable("File", "employee");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.FileSize)
                .HasMaxLength(50)
                .HasColumnName("file_size");
            entity.Property(e => e.FileName)
                .HasMaxLength(50)
                .HasColumnName("file_name");
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.ToTable("Holiday", "employee");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Leaves>(entity =>
        {
            entity.ToTable("leaves", "employee");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Approved).HasColumnName("approved");
            entity.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
            entity.Property(e => e.Days).HasColumnName("days");
            // entity.Property(e => e.Status).HasColumnName("Status")
            //     .HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.FromDate).HasColumnName("from");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .HasColumnName("reason");
            entity.Property(e => e.ToDate).HasColumnName("to");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.ApprovedByNavigation)
                .WithMany(p => p.LeafApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_leaves_Employee_approved");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.LeafEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_leaves_Employee");
        });

        modelBuilder.Entity<Overtime>(entity =>
        {
            entity.ToTable("Overtimes", "employee");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.OtDate).HasColumnName("ot_date");
            entity.Property(e => e.OtHours).HasColumnName("ot_hours");
            entity.Property(e => e.OtType)
                .HasMaxLength(50)
                .HasColumnName("ot_type");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.OvertimeApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_Overtimes_Employee_approve");

            entity.HasOne(d => d.Employee).WithMany(p => p.OvertimeEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Overtimes_Employee");
        });

        modelBuilder.Entity<Scheduling>(entity =>
        {
            entity.ToTable("scheduling", "employee");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.BreakTime).HasColumnName("break_time");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DepartmentId).HasColumnName("department_Id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.MaxEndTime).HasColumnName("max_end_time");
            entity.Property(e => e.MaxStartTime).HasColumnName("max_start_time");
            entity.Property(e => e.MinEndTime).HasColumnName("min_end_time");
            entity.Property(e => e.MinStartTime).HasColumnName("min_start_time");
           
            entity.Property(e => e.RepeatEvery).HasColumnName("repeat_every");
            entity.Property(e => e.ShiftId).HasColumnName("shift_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.HasOne(d => d.Department).WithMany(p => p.Schedulings)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_scheduling_Department");

            // entity.HasOne(d => d.Employee).WithMany(p => p.SchedulingEmployees)
            //     .HasForeignKey(d => d.EmployeeId)
            //     .HasConstraintName("FK_scheduling_Employee_1");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("Shift", "employee");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
            entity.Property(e => e.BreakeTime).HasColumnName("breake_time");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.Indefinate).HasColumnName("indefinate");
            entity.Property(e => e.MaxEndTime).HasColumnName("max_end_time");
            entity.Property(e => e.MaxStartTime).HasColumnName("max_start_time");
            entity.Property(e => e.MinEndTime).HasColumnName("min_end_time");
            entity.Property(e => e.MinStartTime).HasColumnName("min_start_time");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasMaxLength(50)
                .HasColumnName("note");
            entity.Property(e => e.RecurringShift).HasColumnName("recurring_shift");
            entity.Property(e => e.RepeatEvery).HasColumnName("repeat_every");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Tag)
                .HasMaxLength(50)
                .HasColumnName("tag");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_Shift_Employee");
        });

        modelBuilder.Entity<TimeSheet>(entity =>
        {
            entity.ToTable("Timesheet", "employee");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.AssignedHours).HasColumnName("assigned_hours");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Deadline)
                .HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Hours).HasColumnName("hours");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");

            entity.HasOne(d => d.Employee).WithOne(p => p.Timesheet)
                .HasForeignKey<TimeSheet>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timesheet_Employee");
        });
        
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
        
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client", "project");
        
           
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            
            // client permissions relationship

            entity.HasMany(d => d.Permissions)
                .WithOne(p => p.Client)
                .HasForeignKey(e=>e.ClientId);
        });
        
        modelBuilder.Entity<Project>(entity=>
            
        {
        entity.ToTable("Project", "project");
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name");
        entity.Property(e => e.Description)
            .HasColumnName("description");
        entity.Property(e => e.StartDate).HasColumnName("start_date");
        entity.Property(e => e.EndDate).HasColumnName("end_date");
        // entity.Property(e => e.ClientId).HasColumnName("client_id");
        entity.Property(e => e.Priority)
            .HasMaxLength(50)
            .HasColumnName("priority");
        entity.Property(e => e.RateSelect)
            .HasMaxLength(100)
            .HasColumnName("rate_select");
        entity.Property(e => e.Rate).HasPrecision(5,2).HasColumnName("rate");
        entity.Property(e => e.Status).HasColumnName("status");
        entity.Property(e => e.Checked).HasColumnName("checked");
        
        entity.HasOne(d => d.Client).WithMany(p => p.Projects)
            .HasForeignKey(d => d.ClientId)
            .HasConstraintName("FK_Project_Client");
        // leader id 
        entity.HasOne(d => d.Leader).WithMany(p => p.Projects)
            .HasForeignKey(d => d.LeaderId)
            .HasConstraintName("FK_Project_Employee");
        
        // project and employeeporject relation 
        entity.HasMany(d => d.EmployeesProject) 
            .WithOne(p => p.Project)
            .HasForeignKey(e=>e.ProjectId).OnDelete(DeleteBehavior.Cascade);        
        });
        
        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("Task", "project");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.Description)
                .HasColumnName("description");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Priority)
                .HasMaxLength(50)
                .HasColumnName("priority");
            entity.Property(e => e.Completed).HasColumnName("completed");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
        });
        
        modelBuilder.Entity<TaskList>(entity =>
        {
            entity.ToTable("TaskList", "project");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ListName)
                .HasMaxLength(100)
                .HasColumnName("name");
        });
        
        modelBuilder.Entity<TaskBoard>(entity =>
        {
            entity.ToTable("Taskboard", "project");
            
        });
        
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket", "project");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .HasColumnName("subject");
            entity.Property(e => e.Description)
                .HasColumnName("description");
            entity.Property(e => e.Priority)
                .HasMaxLength(50)
                .HasColumnName("priority");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Cc)
                .HasMaxLength(100)
                .HasColumnName("cc");
            entity.Property(e => e.AssignedToEmployeeId).HasColumnName("assigned_to");
            entity.Property(e => e.CreatedByEmployeeId).HasColumnName("created_by");
        entity.Property(e => e.ClientId).HasColumnName("client_id");
        entity.HasOne(d => d.Client).WithMany(p => p.Tickets)
            .HasForeignKey(d => d.ClientId)
            .HasConstraintName("FK_Ticket_Clients");
        entity.HasOne(t=> t.AssignedTo).WithMany()
            .HasForeignKey(t=>t.AssignedToEmployeeId)
            .HasConstraintName("FK_Ticket_Employee_AssignedTo");
        entity.HasOne(t=>t.CreatedBy).WithMany()
            .HasForeignKey(t=>t.CreatedByEmployeeId)
            .HasConstraintName("FK_Ticket_Employee_CreatedBy");
        });
        modelBuilder.Entity<TicketFollowers>(entity =>
        {
            entity.ToTable("TicketFollowers", "project");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            // entity.HasOne(d => d.Employee).WithMany(p => p.TicketFollowers)
                // .HasForeignKey(d => d.EmployeeId)
                // .HasConstraintName("FK_TicketFollowers_Employee");
            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketFollowers)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK_TicketFollowers_Ticket");
        });
        modelBuilder.Entity<Notes>(entity =>
        {
            entity.ToTable("Notes", "employee");
            
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.Content)
                .HasColumnName("content");
            entity.Property(e => e.Starred).HasColumnName("starred");
            entity.Property(e => e.Date).HasColumnName("date");
            // entity.HasOne(d => d.Sender).WithMany(p => p.NotesSender)
            //     .HasForeignKey(d => d.SenderId)
            //     .HasConstraintName("FK_Notes_Employee_Sender");
            // entity.HasOne(d => d.Receiver).WithMany(p => p.NotesReceiver)
            //     .HasForeignKey(d => d.ReceiverId)
            //     .HasConstraintName("FK_Notes_Employee_Receiver");
        });

        modelBuilder.Entity<CustomPolicy>(entity =>
        {
            entity.ToTable("CustomPolicy", "employee");
            
            
        });
        
        modelBuilder.Entity<Estimate>(entity => 
        {
            entity.ToTable("Estimate", "client");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.ClientAddress)
                
                .HasColumnName("client_address");
            entity.Property(e => e.BillingAddress)
                
                .HasColumnName("billing_address");
            entity.Property(e => e.EstimateDate).HasColumnName("estimate_date");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.OtherInformation)
                .HasColumnName("other_information");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.EstimateNumber)
                .HasMaxLength(50)
                .HasColumnName("estimate_number");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Tax).HasColumnName("tax");
            entity.Property(e => e.GrandTotal).HasColumnName("grand_total");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            // entity.HasOne(d => d.Client).WithMany(p => p.Estimates)
            //     .HasForeignKey(d => d.ClientId)
            //     .HasConstraintName("FK_Estimate_Client");
            // entity.HasOne(d => d.Project).WithMany(p => p.Estimates)
            //     .HasForeignKey(d => d.ProjectId)
            //     .HasConstraintName("FK_Estimate_Project");
            entity.HasMany(d => d.Items) 
                .WithOne(p => p.Estimate)
                .HasForeignKey(e=>e.EstimateId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Invoice>(builder =>
        {
            builder.ToTable("Invoice", "client");
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            builder.Property(e => e.ClientAddress)
                .HasColumnName("client_address");
            builder.Property(e => e.BillingAddress)
                .HasColumnName("billing_address");
            builder.Property(e => e.InvoiceDate).HasColumnName("invoice_date");
            builder.Property(e => e.DueDate).HasColumnName("due_date");
            builder.Property(e => e.OtherInformation)
                .HasColumnName("other_information");
            builder.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            builder.Property(e => e.Notes)
                .HasColumnName("notes");
            builder.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .HasColumnName("invoice_number");
            builder.Property(e => e.TotalAmount).HasColumnName("total_amount");
            builder.Property(e => e.Discount).HasColumnName("discount");
            builder.Property(e => e.GrandTotal).HasColumnName("grand_total");
            builder.Property(e => e.ClientId).HasColumnName("client_id");
            builder.Property(e => e.ProjectId).HasColumnName("project_id");
            // builder.HasOne(d => d.Client).WithMany(p => p.Invoices)
            //     .HasForeignKey(d => d.ClientId)
            //     .HasConstraintName("FK_Invoice_Client");
            // builder.HasOne(d => d.Project).WithMany(p => p.Invoices)
            //     .HasForeignKey(d => d.ProjectId)
            //     .HasConstraintName("FK_Invoice_Project");
            builder.HasMany(d => d.Items)
                .WithOne(p => p.Invoice)
                .HasForeignKey(e => e.InvoiceId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Expenses>(entity =>
        {
            entity.ToTable("Expenses", "client");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .HasColumnName("item_name");
            entity.Property(e => e.PurchaseFrom)
                .HasMaxLength(100)
                .HasColumnName("purchase_from");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            // entity.Property(e => e.PurchasedBy).HasColumnName("purchased_by");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.PaidBy)
                .HasMaxLength(100)
                .HasColumnName("paid_by");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            // entity.HasOne(d => d.Client).WithMany(p => p.Expenses)
            //     .HasForeignKey(d => d.ClientId)
            //     .HasConstraintName("FK_Expenses_Client");
            entity.HasMany(d => d.Attachments)
                .WithOne(p => p.Expenses)
                .HasForeignKey(e => e.ExpensesId).OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Permission>()
            .HasOne(p => p.ApplicationUser)
            .WithMany(u => u.Permissions)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    
    
    private static LambdaExpression CreateIsDeletedFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
        var comparison = Expression.MakeBinary(ExpressionType.Equal, property, Expression.Constant(false));
        var lambda = Expression.Lambda(comparison, parameter);
        return lambda;
    }
    
    private static void SetTenantIdFilter<T>(ModelBuilder builder,int tenantId) where T : BaseEntity
    {
        builder.Entity<T>().HasQueryFilter(e => e.TenantId == tenantId);
    }

    private static int? GetCurrentTenantId()
    {
        
        return TenantId;
    }
    private static void SetTenantIdFilter<T>(ModelBuilder modelBuilder) where T : class, IBaseEntity
    {
        modelBuilder.Entity<T>().HasQueryFilter(e => e.TenantId == GetCurrentTenantId());
    }

    private static readonly MethodInfo SetTenantIdFilterMethod = typeof(HrSystemDbContext)
        .GetMethod(nameof(SetTenantIdFilter), BindingFlags.NonPublic | BindingFlags.Static, null,
            new[] { typeof(ModelBuilder) }, null);
}

