using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
{
    public void Configure(EntityTypeBuilder<TaskList> builder)
    {
        builder.ToTable("TaskList", "project");

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.ListName)
            .HasMaxLength(100)
            .HasColumnName("name");
    }
}