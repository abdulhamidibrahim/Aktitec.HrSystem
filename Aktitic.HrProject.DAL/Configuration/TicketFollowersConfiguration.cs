using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class TicketFollowersConfiguration : IEntityTypeConfiguration<TicketFollowers>
{
    public void Configure(EntityTypeBuilder<TicketFollowers> builder)
    {
        builder.ToTable("TicketFollowers", "project");

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.TicketId).HasColumnName("ticket_id");
        builder.Property(e => e.EmployeeId).HasColumnName("employee_id");
        // builder.HasOne(d => d.Employee).WithMany(p => p.TicketFollowers)
        // .HasForeignKey(d => d.EmployeeId)
        // .HasConstraintName("FK_TicketFollowers_Employee");
        builder.HasOne(d => d.Ticket).WithMany(p => p.TicketFollowers)
            .HasForeignKey(d => d.TicketId)
            .HasConstraintName("FK_TicketFollowers_Ticket");
    }
}