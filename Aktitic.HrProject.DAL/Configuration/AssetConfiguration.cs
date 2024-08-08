using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AssetName).IsRequired();
        builder.Property(x => x.AssetId).IsRequired();
        builder.Property(x => x.PurchaseFrom);
        builder.Property(x => x.PurchaseTo);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.Warranty).HasPrecision(10,2);
        builder.Property(x => x.Manufacturer).HasMaxLength(50);
        builder.Property(x => x.Model).HasMaxLength(50);
        builder.Property(x => x.SerialNumber).HasMaxLength(100);
        builder.Property(x => x.Supplier).HasMaxLength(100);
        builder.Property(x => x.Condition).HasMaxLength(50);
        builder.Property(x => x.Value).HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(500);
        
        builder.HasOne(x=>x.User)
            .WithMany(x=>x.Assets)
            .HasForeignKey(x=>x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}