using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class SchedulingConfiguration : IEntityTypeConfiguration<Scheduling>
{
    public void Configure(EntityTypeBuilder<Scheduling> builder)
    {
        builder.ToTable("scheduling", "employee");

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.BreakTime).HasColumnName("break_time");
        builder.Property(e => e.Date).HasColumnName("date");
        builder.Property(e => e.DepartmentId).HasColumnName("department_Id");
        builder.Property(e => e.EmployeeId).HasColumnName("employee_id");
        builder.Property(e => e.EndTime).HasColumnName("end_time");
        builder.Property(e => e.MaxEndTime).HasColumnName("max_end_time");
        builder.Property(e => e.MaxStartTime).HasColumnName("max_start_time");
        builder.Property(e => e.MinEndTime).HasColumnName("min_end_time");
        builder.Property(e => e.MinStartTime).HasColumnName("min_start_time");
           
        builder.Property(e => e.RepeatEvery).HasColumnName("repeat_every");
        builder.Property(e => e.ShiftId).HasColumnName("shift_id");
        builder.Property(e => e.StartTime).HasColumnName("start_time");
        builder.HasOne(d => d.Department).WithMany(p => p.Schedulings)
            .HasForeignKey(d => d.DepartmentId)
            .HasConstraintName("FK_scheduling_Department");
    }
}