using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.BL;

public class InterviewQuestionsUpdateDto
{
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
    public IFormFile? Image { get; set; }

}
