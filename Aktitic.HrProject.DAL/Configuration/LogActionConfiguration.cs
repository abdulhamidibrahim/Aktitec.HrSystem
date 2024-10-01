using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class LogActionConfiguration : IEntityTypeConfiguration<LogAction>
{
    public void Configure(EntityTypeBuilder<LogAction> builder)
    {
        builder.ToTable("LogActions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ArabicName).HasMaxLength(50).IsRequired();
    }
}