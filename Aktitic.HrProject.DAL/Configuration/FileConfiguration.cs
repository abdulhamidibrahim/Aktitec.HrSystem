using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;
//
// public class FileConfiguration : IEntityTypeConfiguration<Document>
// {
//     public void Configure(EntityTypeBuilder<Document> builder)
//     {
//        
//             builder.ToTable("Documents", "employee");
//
//             builder.Property(e => e.Id).ValueGeneratedOnAdd();
//             builder.Property(e => e.Status).HasColumnName("status");
//             // builder.Property(e => e.FileSize)
//             //     .HasMaxLength(50)
//             //     .HasColumnName("file_size");
//             // builder.Property(e => e.FileName)
//             //     .HasMaxLength(50)
//             //     .HasColumnName("file_name");
//
//     }
// }