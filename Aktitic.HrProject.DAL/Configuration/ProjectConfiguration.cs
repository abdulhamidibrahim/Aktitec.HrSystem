using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project", "project");
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name");
        builder.Property(e => e.Description)
            .HasColumnName("description");
        builder.Property(e => e.StartDate).HasColumnName("start_date");
        builder.Property(e => e.EndDate).HasColumnName("end_date");
        // builder.Property(e => e.ClientId).HasColumnName("client_id");
        builder.Property(e => e.Priority)
            .HasMaxLength(50)
            .HasColumnName("priority");
        builder.Property(e => e.RateSelect)
            .HasMaxLength(100)
            .HasColumnName("rate_select");
        builder.Property(e => e.Rate).HasPrecision(5,2).HasColumnName("rate");
        builder.Property(e => e.Status).HasColumnName("status");
        builder.Property(e => e.Checked).HasColumnName("checked");
        
        builder.HasOne(d => d.Client).WithMany(p => p.Projects)
            .HasForeignKey(d => d.ClientId)
            .HasConstraintName("FK_Project_Client");
        // leader id 
        builder.HasOne(d => d.Leader).WithMany(p => p.Projects)
            .HasForeignKey(d => d.LeaderId)
            .HasConstraintName("FK_Project_Employee");
        
        // project and employeeporject relation 
        builder.HasMany(d => d.EmployeesProject) 
            .WithOne(p => p.Project)
            .HasForeignKey(e=>e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);       
        
        // project and taskboard relation
        builder.HasOne(d => d.TaskBoard).WithOne(p => p.Project)
            .HasForeignKey<Project>(d => d.TaskBoardId)
            .HasConstraintName("FK_Project_TaskBoard")
            .OnDelete(DeleteBehavior.Cascade);
    }
}