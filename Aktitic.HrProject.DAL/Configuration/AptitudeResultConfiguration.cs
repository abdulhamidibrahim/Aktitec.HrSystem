using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class AptitudeResultConfiguration : IEntityTypeConfiguration<AptitudeResult>
{
    public void Configure(EntityTypeBuilder<AptitudeResult> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.Property(e => e.JobId)
            .IsRequired();

        builder.Property(e => e.CategoryWiseMark)
            .HasMaxLength(100);

        builder.Property(e => e.TotalMark)
            .HasMaxLength(100);

        builder.Property(e => e.Status)
            .HasMaxLength(100);
        
        builder.HasOne(e => e.Employee)
            .WithMany(e => e.AptitudeResults)
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(e => e.Job)
            .WithMany(e => e.AptitudeResults)
            .HasForeignKey(e => e.JobId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}