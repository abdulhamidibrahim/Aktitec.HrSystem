using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.ToTable("Shift", "employee");

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
        builder.Property(e => e.BreakeTime).HasColumnName("breake_time");
        builder.Property(e => e.EndDate).HasColumnName("end_date");
        builder.Property(e => e.EndTime).HasColumnName("end_time");
        builder.Property(e => e.Indefinate).HasColumnName("indefinate");
        builder.Property(e => e.MaxEndTime).HasColumnName("max_end_time");
        builder.Property(e => e.MaxStartTime).HasColumnName("max_start_time");
        builder.Property(e => e.MinEndTime).HasColumnName("min_end_time");
        builder.Property(e => e.MinStartTime).HasColumnName("min_start_time");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
        builder.Property(e => e.Note)
            .HasMaxLength(50)
            .HasColumnName("note");
        builder.Property(e => e.RecurringShift).HasColumnName("recurring_shift");
        builder.Property(e => e.RepeatEvery).HasColumnName("repeat_every");
        builder.Property(e => e.StartTime).HasColumnName("start_time");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");
        builder.Property(e => e.Tag)
            .HasMaxLength(50)
            .HasColumnName("tag");

        builder.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.Shifts)
            .HasForeignKey(d => d.ApprovedBy)
            .HasConstraintName("FK_Shift_Employee");
    }
}