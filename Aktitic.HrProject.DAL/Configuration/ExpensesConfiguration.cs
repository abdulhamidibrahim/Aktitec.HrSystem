using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class ExpensesConfiguration : IEntityTypeConfiguration<Expenses>
{
    public void Configure(EntityTypeBuilder<Expenses> builder)
    {
        builder.ToTable("Expenses", "client");
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.ItemName)
            .HasMaxLength(100)
            .HasColumnName("item_name");
        builder.Property(e => e.PurchaseFrom)
            .HasMaxLength(100)
            .HasColumnName("purchase_from");
        builder.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
        // entity.Property(e => e.PurchasedBy).HasColumnName("purchased_by");
        builder.Property(e => e.Amount).HasColumnName("amount");
        builder.Property(e => e.PaidBy)
            .HasMaxLength(100)
            .HasColumnName("paid_by");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");
        // entity.HasOne(d => d.Client).WithMany(p => p.Expenses)
        //     .HasForeignKey(d => d.ClientId)
        //     .HasConstraintName("FK_Expenses_Client");
        // builder.HasMany(d => d.Attachments)
        //     .WithOne(p => p.Expenses)
        //     .HasForeignKey(e => e.ExpensesId).OnDelete(DeleteBehavior.Cascade);
    }
}