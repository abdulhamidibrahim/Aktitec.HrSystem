using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Client", "project");
        
           
        builder.Property(e => e.Email)
            .HasMaxLength(100)
            .HasColumnName("email");
        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .HasColumnName("first_name");
        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .HasColumnName("last_name");
        builder.Property(e => e.Phone)
            .HasMaxLength(50)
            .HasColumnName("phone");
            
        // client permissions relationship

        // builder.HasMany(d => d.Permissions)
        //     .WithOne(p => p.Client)
        //     .HasForeignKey(e=>e.ClientId);
    }
}