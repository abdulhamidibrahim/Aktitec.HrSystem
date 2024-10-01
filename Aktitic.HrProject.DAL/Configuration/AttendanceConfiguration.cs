using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable("Attendance", "employee");

        builder.Property(e => e.Break).HasColumnName("break");
        builder.Property(e => e.Date).HasColumnName("date");
        builder.Property(e => e.EmployeeId).HasColumnName("employee_id");
        // builder.Property(e => e.OvertimeId).HasColumnName("overtime_id");
        builder.Property(e => e.Production).HasColumnName("production");
        builder.Property(e => e.PunchIn)
            .HasColumnType("datetime")
            .HasColumnName("punch_in");
        builder.Property(e => e.PunchOut)
            .HasColumnType("datetime")
            .HasColumnName("punch_out");

        builder.HasOne(d => d.Employee)
            .WithMany(p => p.Attendances)
            .HasForeignKey(d => d.EmployeeId)
            .HasConstraintName("FK_Attendance_Employee");

        // builder.HasOne(d => d.Overtime).WithMany(p => p.Attendances)
        //     .HasForeignKey(d => d.OvertimeId)
        //     .HasConstraintName("FK_Attendance_Overtimes");
    }
}