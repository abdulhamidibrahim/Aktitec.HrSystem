using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Ticket", "project");

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Subject)
            .HasMaxLength(100)
            .HasColumnName("subject");
        builder.Property(e => e.Description)
            .HasColumnName("description");
        builder.Property(e => e.Priority)
            .HasMaxLength(50)
            .HasColumnName("priority");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");
        builder.Property(e => e.Cc)
            .HasMaxLength(100)
            .HasColumnName("cc");
        builder.Property(e => e.AssignedToEmployeeId).HasColumnName("assigned_to");
        builder.Property(e => e.CreatedByEmployeeId).HasColumnName("created_by");
        builder.Property(e => e.ClientId).HasColumnName("client_id");
        builder.HasOne(d => d.Client).WithMany(p => p.Tickets)
            .HasForeignKey(d => d.ClientId)
            .HasConstraintName("FK_Ticket_Clients");
        builder.HasOne(t=> t.AssignedTo).WithMany()
            .HasForeignKey(t=>t.AssignedToEmployeeId)
            .HasConstraintName("FK_Ticket_Employee_AssignedTo");
        builder.HasOne(t=>t.CreatedBy).WithMany()
            .HasForeignKey(t=>t.CreatedByEmployeeId)
            .HasConstraintName("FK_Ticket_Employee_CreatedBy");
    }
}