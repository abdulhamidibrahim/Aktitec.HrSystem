using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aktitic.HrProject.DAL.Configuration;

public class InterviewQuestionConfiguration : IEntityTypeConfiguration<InterviewQuestion>
{
    public void Configure(EntityTypeBuilder<InterviewQuestion> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DepartmentId).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DepartmentId).IsRequired();
        builder.Property(x => x.Question).IsRequired().HasMaxLength(500);
        builder.Property(x => x.OptionA).IsRequired().HasMaxLength(100);
        builder.Property(x => x.OptionB).IsRequired().HasMaxLength(100);
        builder.Property(x => x.OptionC).IsRequired().HasMaxLength(100);
        builder.Property(x => x.OptionD).IsRequired().HasMaxLength(100);
        builder.Property(x => x.CorrectAnswer).IsRequired();
        builder.Property(x => x.CodeSnippets).HasMaxLength(300);
        builder.Property(x => x.AnswerExplanation).HasMaxLength(500);
        builder.Property(x => x.VideoLink).HasMaxLength(200);
        builder.Property(x => x.Image).HasMaxLength(100);

        builder.HasOne(x => x.Department)
            .WithMany(x=>x.InterviewQuestions)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}