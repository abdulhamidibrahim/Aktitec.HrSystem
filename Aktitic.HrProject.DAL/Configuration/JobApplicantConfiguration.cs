using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class JobApplicantConfiguration : IEntityTypeConfiguration<JobApplicant>
{
    public void Configure(EntityTypeBuilder<JobApplicant> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Phone).HasMaxLength(22).IsRequired();
        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.Status).HasMaxLength(100);
        builder.Property(e => e.Resume).HasMaxLength(100);
    }
}