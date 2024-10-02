using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class NotesConfiguration : IEntityTypeConfiguration<Notes>
{
    public void Configure(EntityTypeBuilder<Notes> builder)
    {
        builder.ToTable("Notes", "employee");
            
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.SenderId).HasColumnName("sender_id");
        builder.Property(e => e.ReceiverId).HasColumnName("receiver_id");
        builder.Property(e => e.Content)
            .HasColumnName("content");
        builder.Property(e => e.Starred).HasColumnName("starred");
        builder.Property(e => e.Date).HasColumnName("date");
        // builder.HasOne(d => d.Sender).WithMany(p => p.NotesSender)
        //     .HasForeignKey(d => d.SenderId)
        //     .HasConstraintName("FK_Notes_Employee_Sender");
        // builder.HasOne(d => d.Receiver).WithMany(p => p.NotesReceiver)
        //     .HasForeignKey(d => d.ReceiverId)
        //     .HasConstraintName("FK_Notes_Employee_Receiver");
    }
}