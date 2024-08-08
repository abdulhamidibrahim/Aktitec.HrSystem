using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class OfferApprovalConfiguration : IEntityTypeConfiguration<OfferApproval>
{
    public void Configure(EntityTypeBuilder<OfferApproval> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.Property(e => e.JobId)
            .IsRequired();

        builder.Property(e => e.Pay)
            .HasMaxLength(50);

        builder.Property(e => e.AnnualIp)
            .HasMaxLength(50);

        builder.Property(e => e.Status)
            .HasMaxLength(50);

        builder.HasOne(e => e.Employee)
            .WithMany(e => e.OfferApprovals)
            .HasForeignKey(e => e.EmployeeId);

        builder.HasOne(e => e.Job)
            .WithMany(e => e.OfferApprovals)
            .HasForeignKey(e => e.JobId);
    }
}