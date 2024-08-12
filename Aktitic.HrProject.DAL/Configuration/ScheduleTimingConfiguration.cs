using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class ScheduleTimingConfiguration : IEntityTypeConfiguration<ScheduleTiming>
{
    public void Configure(EntityTypeBuilder<ScheduleTiming> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.SelectTime1).HasMaxLength(100);
        builder.Property(x => x.SelectTime2).HasMaxLength(100);
        builder.Property(x => x.SelectTime3).HasMaxLength(100);
        
        builder.HasOne(x => x.Employee)
            .WithMany(x => x.ScheduleTimings)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Job)
            .WithMany(x => x.ScheduleTimings)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}