using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class ModifiedRecordConfiguration : IEntityTypeConfiguration<ModifiedRecord>
{
    public void Configure(EntityTypeBuilder<ModifiedRecord> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RecordId).IsRequired();
        builder.Property(x => x.PermenantlyDeleted).IsRequired();
        builder.Property(x => x.PermenantlyDeletedBy).HasMaxLength(50);
        
        
        builder.HasOne(x => x.AuditLog)
            .WithMany(x => x.ModifiedRecords)
            .HasForeignKey(x => x.AuditLogId)
            .OnDelete(DeleteBehavior.Cascade);
        
      
    }
}
