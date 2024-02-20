using System;
using System.Collections.Generic;
using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.DAL.Context;

public partial class HrManagementDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
{
    public HrManagementDbContext()
    {
    }

    public HrManagementDbContext(DbContextOptions<HrManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<File?> Files { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<Leaves> Leaves { get; set; }

    public virtual DbSet<Overtime> Overtimes { get; set; }

    public virtual DbSet<Scheduling> Schedulings { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<Timesheet> Timesheets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=127.0.0.1,1433\\;Database=HrManagement;User Id=SA;Password={Mido76145720@@};TrustServerCertificate=True;MultiSubnetFailover=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.ToTable("Attendance", "employee");

            entity.Property(e => e.Break).HasColumnName("break");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.OvertimeId).HasColumnName("overtime_id");
            entity.Property(e => e.Production).HasColumnName("production");
            entity.Property(e => e.PunchIn)
                .HasColumnType("datetime")
                .HasColumnName("punch_in");
            entity.Property(e => e.PunchOut)
                .HasColumnType("datetime")
                .HasColumnName("punch_out");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Attendance_Employee");

            entity.HasOne(d => d.Overtime).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.OvertimeId)
                .HasConstraintName("FK_Attendance_Overtimes");
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

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.Department).WithMany(p => p.Designations)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Designation_Department");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee", "employee");

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

            entity.HasOne(d => d.Img).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ImgId)
                .HasConstraintName("FK_Employee_File_1");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.ToTable("File", "employee");

            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Extension)
                .HasMaxLength(50)
                .HasColumnName("extension");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.ToTable("Holiday", "employee");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Leaves>(entity =>
        {
            entity.ToTable("leaves", "employee");

            entity.Property(e => e.Approved).HasColumnName("approved");
            entity.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
            entity.Property(e => e.Days).HasColumnName("days");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.FromDate).HasColumnName("from");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .HasColumnName("reason");
            entity.Property(e => e.ToDate).HasColumnName("to");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.LeafApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_leaves_Employee_approved");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeafEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_leaves_Employee");
        });

        modelBuilder.Entity<Overtime>(entity =>
        {
            entity.ToTable("Overtimes", "employee");

            entity.Property(e => e.Id).ValueGeneratedNever();
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

            entity.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
            entity.Property(e => e.BreakTime).HasColumnName("break_time");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DepartmentId).HasColumnName("department_Id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.MaxEndTime).HasColumnName("max_end_time");
            entity.Property(e => e.MaxStartTime).HasColumnName("max_start_time");
            entity.Property(e => e.MinEndTime).HasColumnName("min_end_time");
            entity.Property(e => e.MinStartTime).HasColumnName("min_start_time");
            entity.Property(e => e.Note)
                .HasMaxLength(50)
                .HasColumnName("note");
            entity.Property(e => e.RepeatEvery).HasColumnName("repeat_every");
            entity.Property(e => e.ShiftId).HasColumnName("shift_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.SchedulingApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_scheduling_Employee_2");

            entity.HasOne(d => d.Department).WithMany(p => p.Schedulings)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_scheduling_Department");

            entity.HasOne(d => d.Employee).WithMany(p => p.SchedulingEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_scheduling_Employee_1");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("Shift", "employee");

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

        modelBuilder.Entity<Timesheet>(entity =>
        {
            entity.ToTable("Timesheet", "employee");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.AssignedHours).HasColumnName("assigned_hours");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Hours).HasColumnName("hours");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Timesheet)
                .HasForeignKey<Timesheet>(d => d.Id)
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
        
        // modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
        // {
        //     entity.HasKey(e => new { e.Id });
        // });
        //
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
