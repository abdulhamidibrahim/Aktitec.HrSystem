using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class TimesheetConfiguration : IEntityTypeConfiguration<TimeSheet>
{
    public void Configure(EntityTypeBuilder<TimeSheet> builder)
    {
        builder.ToTable("Timesheet", "employee");

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.AssignedHours).HasColumnName("assigned_hours");
        builder.Property(e => e.Date).HasColumnName("date");
        builder.Property(e => e.Deadline)
            .HasColumnName("deadline");
        builder.Property(e => e.Description).HasColumnName("description");
        builder.Property(e => e.EmployeeId).HasColumnName("employee_id");
        builder.Property(e => e.Hours).HasColumnName("hours");
        builder.Property(e => e.ProjectId).HasColumnName("project_id");

        builder.HasOne(d => d.Employee).WithOne(p => p.Timesheet)
            .HasForeignKey<TimeSheet>(d => d.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Timesheet_Employee");
    }
}