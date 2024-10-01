using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoice", "client");
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Email)
            .HasMaxLength(100)
            .HasColumnName("email");
        builder.Property(e => e.ClientAddress)
            .HasColumnName("client_address");
        builder.Property(e => e.BillingAddress)
            .HasColumnName("billing_address");
        builder.Property(e => e.InvoiceDate).HasColumnName("invoice_date");
        builder.Property(e => e.DueDate).HasColumnName("due_date");
        builder.Property(e => e.OtherInformation)
            .HasColumnName("other_information");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");
        builder.Property(e => e.Notes)
            .HasColumnName("notes");
        builder.Property(e => e.InvoiceNumber)
            .HasMaxLength(50)
            .HasColumnName("invoice_number");
        builder.Property(e => e.TotalAmount).HasColumnName("total_amount");
        builder.Property(e => e.Discount).HasColumnName("discount");
        builder.Property(e => e.GrandTotal).HasColumnName("grand_total");
        builder.Property(e => e.ClientId).HasColumnName("client_id");
        builder.Property(e => e.ProjectId).HasColumnName("project_id");
        // builder.HasOne(d => d.Client).WithMany(p => p.Invoices)
        //     .HasForeignKey(d => d.ClientId)
        //     .HasConstraintName("FK_Invoice_Client");
        // builder.HasOne(d => d.Project).WithMany(p => p.Invoices)
        //     .HasForeignKey(d => d.ProjectId)
        //     .HasConstraintName("FK_Invoice_Project");
        builder.HasMany(d => d.Items)
            .WithOne(p => p.Invoice)
            .HasForeignKey(e => e.InvoiceId).OnDelete(DeleteBehavior.Cascade);
    }
}