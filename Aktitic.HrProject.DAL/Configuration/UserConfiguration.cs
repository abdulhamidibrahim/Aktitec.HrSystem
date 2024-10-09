using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
            builder.HasOne(x=>x.Role)
                .WithOne(x=>x.User)
                .HasForeignKey<CompanyRole>(x=>x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        
    }
}