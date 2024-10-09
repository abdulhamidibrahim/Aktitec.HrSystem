using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class CompanyModuleConfiguration : IEntityTypeConfiguration<CompanyModule>
{
    public void Configure(EntityTypeBuilder<CompanyModule> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Company)
            .WithMany(x => x.CompanyModules)
            .HasForeignKey(x => x.CompanyId);
        
        builder.HasOne(x => x.AppModule)
            .WithMany(x => x.CompanyModules)
            .HasForeignKey(x => x.AppModuleId);
    }
}