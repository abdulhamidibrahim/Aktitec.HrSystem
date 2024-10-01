using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class EstimateConfiguration : IEntityTypeConfiguration<Estimate>
{
    public void Configure(EntityTypeBuilder<Estimate> builder)
    {
        builder.ToTable("Estimate", "client");
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Email)
            .HasMaxLength(100)
            .HasColumnName("email");
        builder.Property(e => e.ClientAddress)
                
            .HasColumnName("client_address");
        builder.Property(e => e.BillingAddress)
                
            .HasColumnName("billing_address");
        builder.Property(e => e.EstimateDate).HasColumnName("estimate_date");
        builder.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
        builder.Property(e => e.OtherInformation)
            .HasColumnName("other_information");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");
        builder.Property(e => e.EstimateNumber)
            .HasMaxLength(50)
            .HasColumnName("estimate_number");
        builder.Property(e => e.TotalAmount).HasColumnName("total_amount");
        builder.Property(e => e.Discount).HasColumnName("discount");
        builder.Property(e => e.Tax).HasColumnName("tax");
        builder.Property(e => e.GrandTotal).HasColumnName("grand_total");
        builder.Property(e => e.ClientId).HasColumnName("client_id");
        builder.Property(e => e.ProjectId).HasColumnName("project_id");
        // builder.HasOne(d => d.Client).WithMany(p => p.Estimates)
        //     .HasForeignKey(d => d.ClientId)
        //     .HasConstraintName("FK_Estimate_Client");
        // builder.HasOne(d => d.Project).WithMany(p => p.Estimates)
        //     .HasForeignKey(d => d.ProjectId)
        //     .HasConstraintName("FK_Estimate_Project");
        builder.HasMany(d => d.Items) 
            .WithOne(p => p.Estimate)
            .HasForeignKey(e=>e.EstimateId).OnDelete(DeleteBehavior.Cascade);
    }
}