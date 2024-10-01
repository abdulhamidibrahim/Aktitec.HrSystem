using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.Property(x => x.JobTitle).IsRequired().HasMaxLength(50);
        builder.Property(x => x.JobType).IsRequired().HasMaxLength(50);
        builder.Property(x => x.JobLocation).HasMaxLength(100);
        builder.Property(x => x.NoOfVacancies).HasMaxLength(50);
        builder.Property(x => x.Experience).HasMaxLength(50);
        builder.Property(x => x.SalaryFrom).HasPrecision(10,2);
        builder.Property(x => x.SalaryTo).HasPrecision(10,2);
        builder.Property(x => x.Status).HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(50);
        builder.Property(x => x.Category).HasMaxLength(50);
        
        
        builder.HasOne(x => x.Department)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);        
    }
}