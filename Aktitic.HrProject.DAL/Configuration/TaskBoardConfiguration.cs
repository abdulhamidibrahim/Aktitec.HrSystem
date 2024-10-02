using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class TaskBoardConfiguration : IEntityTypeConfiguration<TaskBoard>
{
    public void Configure(EntityTypeBuilder<TaskBoard> builder)
    {
        builder.ToTable("Taskboard", "project");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.ListName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Color).HasMaxLength(50);
        
        builder.HasOne(x=>x.Project)
            .WithOne(x=>x.TaskBoard)
            .HasForeignKey<TaskBoard>(x=>x.ProjectId);
        
    }
}