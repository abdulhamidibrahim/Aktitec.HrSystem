using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class EMailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Subject).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Cc).HasMaxLength(50);
        builder.Property(x => x.Bcc).HasMaxLength(50);
        builder.Property(x => x.ReceiverId).IsRequired();    
        builder.Property(x => x.SenderId).IsRequired();    
        
        
        builder.HasOne(x => x.Sender)
            .WithMany(x => x.SentEmails)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.Receiver)
            .WithMany(x => x.ReceivedEmails)
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}