using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class DesignationConfiguration: IEntityTypeConfiguration<Designation>
{
    public void Configure(EntityTypeBuilder<Designation> builder)
    {
            builder.ToTable("Designation", "employee");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.DepartmentId).HasColumnName("department_id");
            builder.Property(e => e.Name).HasColumnName("name");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Designations)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Designation_Department");
     

    }
}