using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class CustomPolicyConfiguration : IEntityTypeConfiguration<CustomPolicy>
{
    public void Configure(EntityTypeBuilder<CustomPolicy> builder)
    {
        builder.ToTable("CustomPolicy", "employee");
    }
}