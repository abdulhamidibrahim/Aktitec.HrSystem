using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class EmployeeConfiguration: IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> entity)
    {
        entity.ToTable("Employee", "employee");

        // entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.Age).HasColumnName("age");
        entity.Property(e => e.DepartmentId).HasColumnName("department_id");
        entity.Property(e => e.FullName).HasColumnName("full_name");
        entity.Property(e => e.Gender)
            .HasMaxLength(50)
            .HasColumnName("gender");
        entity.Property(e => e.ImgId).HasColumnName("img_id");
        entity.Property(e => e.JobPosition)
            .HasMaxLength(50)
            .HasColumnName("job_position");
        entity.Property(e => e.JoiningDate).HasColumnName("joining_date");
        entity.Property(e => e.ManagerId).HasColumnName("manager_id");
        entity.Property(e => e.Phone)
            .HasMaxLength(50)
            .HasColumnName("phone");
        entity.Property(e => e.Salary)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("salary");
        entity.Property(e => e.YearsOfExperience).HasColumnName("years_of_experience");
        
        entity.HasOne(d => d.Department).WithMany(p => p.Employees)
            .HasForeignKey(d => d.DepartmentId)
            .HasConstraintName("FK_Employee_Department_1");

    }
}