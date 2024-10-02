using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class FamilyInformaitonConfiguration : IEntityTypeConfiguration<FamilyInformation>
{
    public void Configure(EntityTypeBuilder<FamilyInformation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Relationship).IsRequired();
        builder.Property(x => x.Phone).IsRequired();
        builder.Property(x => x.DoB).IsRequired();
        builder.HasOne(x => x.User)
            .WithMany(x => x.FamilyInformations)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x=>x.User)
            .WithMany(x => x.FamilyInformations)
            .HasForeignKey(x=>x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}