using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class OvertimeConfiguration : IEntityTypeConfiguration<Overtime>
{
    public void Configure(EntityTypeBuilder<Overtime> builder)
    {
        builder.ToTable("Overtimes", "employee");

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
        builder.Property(e => e.Description).HasColumnName("description");
        builder.Property(e => e.EmployeeId).HasColumnName("employee_id");
        builder.Property(e => e.OtDate).HasColumnName("ot_date");
        builder.Property(e => e.OtHours).HasColumnName("ot_hours");
        builder.Property(e => e.OtType)
            .HasMaxLength(50)
            .HasColumnName("ot_type");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");

        builder.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.OvertimeApprovedByNavigations)
            .HasForeignKey(d => d.ApprovedBy)
            .HasConstraintName("FK_Overtimes_Employee_approve");

        builder.HasOne(d => d.Employee).WithMany(p => p.OvertimeEmployees)
            .HasForeignKey(d => d.EmployeeId)
            .HasConstraintName("FK_Overtimes_Employee");
    }
}