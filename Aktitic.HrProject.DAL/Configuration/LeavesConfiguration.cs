using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class LeavesConfiguration : IEntityTypeConfiguration<Leaves>
{
    public void Configure(EntityTypeBuilder<Leaves> builder)
    {
        builder.ToTable("leaves", "employee");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Approved).HasColumnName("approved");
        builder.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
        builder.Property(e => e.Days).HasColumnName("days");
        // builder.Property(e => e.Confidential).HasColumnName("Confidential")
        //     .HasMaxLength(50);
        builder.Property(e => e.EmployeeId).HasColumnName("employee_id");
        builder.Property(e => e.FromDate).HasColumnName("from");
        builder.Property(e => e.Reason)
            .HasMaxLength(50)
            .HasColumnName("reason");
        builder.Property(e => e.ToDate).HasColumnName("to");
        builder.Property(e => e.Type)
            .HasMaxLength(50)
            .HasColumnName("type");

        builder.HasOne(d => d.ApprovedByNavigation)
            .WithMany(p => p.LeafApprovedByNavigations)
            .HasForeignKey(d => d.ApprovedBy)
            .HasConstraintName("FK_leaves_Employee_approved");

        builder.HasOne(d => d.Employee)
            .WithMany(p => p.LeafEmployees)
            .HasForeignKey(d => d.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_leaves_Employee");
    }
}