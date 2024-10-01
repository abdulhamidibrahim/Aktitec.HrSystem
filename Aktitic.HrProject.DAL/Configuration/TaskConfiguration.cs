using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Task", "project");

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Text)
            .HasMaxLength(100)
            .HasColumnName("title");
        builder.Property(e => e.Description)
            .HasColumnName("description");
        builder.Property(e => e.Date).HasColumnName("date");
        builder.Property(e => e.Priority)
            .HasMaxLength(50)
            .HasColumnName("priority");
        builder.Property(e => e.Completed).HasColumnName("completed");
        builder.Property(e => e.ProjectId).HasColumnName("project_id");
    }
}