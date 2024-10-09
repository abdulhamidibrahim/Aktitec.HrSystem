using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class AppPagesConfiguration : IEntityTypeConfiguration<AppPages>
{
    public void Configure(EntityTypeBuilder<AppPages> builder)
    {
        builder.ToTable("AppPages");

        builder.HasKey(e => e.Code);

        // builder.Property(e => e.Id)
            // .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.ArabicName)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50);
    }
}