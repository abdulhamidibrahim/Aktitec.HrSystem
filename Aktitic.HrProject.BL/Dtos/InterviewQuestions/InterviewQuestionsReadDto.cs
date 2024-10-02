using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class InterviewQuestionsReadDto
{
    public int Id { get; set; }
    public required string Category { get; set; }
    public required int DepartmentId { get; set; }
    public required string Question { get; set; }
    public required string OptionA { get; set; }
    public required string OptionB { get; set; }
    public required string OptionC { get; set; }
    public required string OptionD { get; set; }
    public required CorrectAnswer CorrectAnswer { get; set; }
    public string? CodeSnippets { get; set; }
    public string? AnswerExplanation { get; set; }
    public string? VideoLink { get; set; }
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
