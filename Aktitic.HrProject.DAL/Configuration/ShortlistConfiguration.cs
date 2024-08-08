using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class ShortlistConfiguration :IEntityTypeConfiguration<Shortlist>
{
    public void Configure(EntityTypeBuilder<Shortlist> builder)
    {
        builder.Property(x => x.Status).HasMaxLength(50);
        
        builder.HasOne(x => x.Job)
            .WithMany(x => x.Shortlists)
            .HasForeignKey(x => x.JobId);        
    }
}