using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using File = System.IO.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class InterviewQuestionsManager(
    // UserUtility userUtility,
    IMapper mapper,
    IWebHostEnvironment webHostEnvironment,
    IUnitOfWork unitOfWork) : IInterviewQuestionsManager
{
    public Task<int> Add(InterviewQuestionsAddDto interviewQuestionsAddDto)
    {
        var interviewQuestions = new InterviewQuestion
        {
            Category = interviewQuestionsAddDto.Category,
            DepartmentId = interviewQuestionsAddDto.DepartmentId,
            Question = interviewQuestionsAddDto.Question,
            OptionA = interviewQuestionsAddDto.OptionA,
            OptionB = interviewQuestionsAddDto.OptionB,
            OptionC = interviewQuestionsAddDto.OptionC,
            OptionD = interviewQuestionsAddDto.OptionD,
            CorrectAnswer = interviewQuestionsAddDto.CorrectAnswer,
            CodeSnippets = interviewQuestionsAddDto.CodeSnippets,
            AnswerExplanation = interviewQuestionsAddDto.AnswerExplanation,
            VideoLink = interviewQuestionsAddDto.VideoLink,
            // CreatedAt = DateTime.Now,
            // CreatedBy = userUtility.GetUserName(),
        };

        if (interviewQuestionsAddDto.Image is not null)
        {
            var unique = Guid.NewGuid();

            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/interviews", unique.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using var fileStream = new FileStream(Path.Combine(path, interviewQuestionsAddDto.Image.FileName), FileMode.Create);
       
            interviewQuestionsAddDto.Image.CopyTo(fileStream);
        
            interviewQuestions.Image = "uploads/contacts"+ unique + "/" + interviewQuestionsAddDto.Image.FileName;

        }

        unitOfWork.InterviewQuestions.Add(interviewQuestions);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(InterviewQuestionsUpdateDto interviewQuestionsUpdateDto, int id)
    {
        var interviewQuestions = unitOfWork.InterviewQuestions.GetById(id);
        
        if (interviewQuestions == null) return Task.FromResult(0);

        interviewQuestions.Category = interviewQuestionsUpdateDto.Category;
        interviewQuestions.DepartmentId = interviewQuestionsUpdateDto.DepartmentId;
        interviewQuestions.Question = interviewQuestionsUpdateDto.Question; 
        interviewQuestions.OptionA = interviewQuestionsUpdateDto.OptionA;
        interviewQuestions.OptionB= interviewQuestionsUpdateDto.OptionB;
        interviewQuestions.OptionC = interviewQuestionsUpdateDto.OptionC;
        interviewQuestions.OptionD = interviewQuestionsUpdateDto.OptionD;
        if (interviewQuestionsUpdateDto.CodeSnippets.IsNullOrEmpty() )
            interviewQuestions.CodeSnippets = interviewQuestionsUpdateDto.CodeSnippets;
        if (interviewQuestionsUpdateDto.AnswerExplanation.IsNullOrEmpty() )
            interviewQuestions.AnswerExplanation = interviewQuestionsUpdateDto.AnswerExplanation;
        if (interviewQuestionsUpdateDto.VideoLink.IsNullOrEmpty() ) 
            interviewQuestions.VideoLink = interviewQuestionsUpdateDto.VideoLink;
        if (interviewQuestionsUpdateDto.CorrectAnswer != interviewQuestions.CorrectAnswer )
            interviewQuestions.VideoLink = interviewQuestionsUpdateDto.VideoLink;

        
        if (interviewQuestionsUpdateDto.Image != null)
        {
            // Construct the path for the current image
            var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, interviewQuestions.Image);

            // Delete the old image file if it exists
            if (File.Exists(oldImagePath))
            {
                try
                {
                    File.Delete(oldImagePath);
                }
                catch (Exception ex)
                {
                    // Log the exception (you might want to use a logging framework)
                    Console.WriteLine($"Failed to delete old image: {ex.Message}");
                }
            }

            // Use the same path for the new image
            var unique = Path.GetDirectoryName(interviewQuestions.Image);
            var path = Path.Combine(webHostEnvironment.WebRootPath, unique);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, interviewQuestionsUpdateDto.Image.FileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            interviewQuestionsUpdateDto.Image.CopyTo(fileStream);

            interviewQuestions.Image = Path.Combine(unique, interviewQuestionsUpdateDto.Image.FileName);
        }
        
        // interviewQuestions.UpdatedAt = DateTime.Now;
        // interviewQuestions.UpdatedBy = userUtility.GetUserName();
        
        unitOfWork.InterviewQuestions.Update(interviewQuestions);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var interviewQuestions = unitOfWork.InterviewQuestions.GetById(id);
        if (interviewQuestions==null) return Task.FromResult(0);
        interviewQuestions.IsDeleted = true;
        // interviewQuestions.DeletedAt = DateTime.Now;
        // interviewQuestions.DeletedBy = userUtility.GetUserName();
        unitOfWork.InterviewQuestions.Update(interviewQuestions);
        return unitOfWork.SaveChangesAsync();
    }

    public InterviewQuestionsReadDto? Get(int id)
    {
        var interviewQuestions = unitOfWork.InterviewQuestions.GetById(id);
        if (interviewQuestions == null) return null;
        
        return new InterviewQuestionsReadDto()
        {
            Id = interviewQuestions.Id,
            Category = interviewQuestions.Category,
            DepartmentId = interviewQuestions.DepartmentId,
            Question = interviewQuestions.Question,
            OptionA = interviewQuestions.OptionA,
            OptionB = interviewQuestions.OptionB,
            OptionC = interviewQuestions.OptionC,
            OptionD = interviewQuestions.OptionD,
            CorrectAnswer = interviewQuestions.CorrectAnswer,
            CodeSnippets = interviewQuestions.CodeSnippets,
            AnswerExplanation = interviewQuestions.AnswerExplanation,
            VideoLink = interviewQuestions.VideoLink,
            Image = interviewQuestions.Image,
            CreatedAt = interviewQuestions.CreatedAt,
            CreatedBy = interviewQuestions.CreatedBy,
            UpdatedAt = interviewQuestions.UpdatedAt,
            UpdatedBy = interviewQuestions.UpdatedBy,    
        };
    }

    public async Task<List<InterviewQuestionsReadDto>> GetAll()
    {
        var interviewQuestions = await unitOfWork.InterviewQuestions.GetAll();
        return interviewQuestions.Select(interviewQuestions => new InterviewQuestionsReadDto()
        {
            Id = interviewQuestions.Id,
            Category = interviewQuestions.Category,
            DepartmentId = interviewQuestions.DepartmentId,
            Question = interviewQuestions.Question,
            OptionA = interviewQuestions.OptionA,
            OptionB = interviewQuestions.OptionB,
            OptionC = interviewQuestions.OptionC,
            OptionD = interviewQuestions.OptionD,
            CorrectAnswer = interviewQuestions.CorrectAnswer,
            CodeSnippets = interviewQuestions.CodeSnippets,
            AnswerExplanation = interviewQuestions.AnswerExplanation,
            VideoLink = interviewQuestions.VideoLink,
            Image = interviewQuestions.Image,
            CreatedAt = interviewQuestions.CreatedAt,
            CreatedBy = interviewQuestions.CreatedBy,
            UpdatedAt = interviewQuestions.UpdatedAt,
            UpdatedBy = interviewQuestions.UpdatedBy,    
            
        }).ToList();
    }

   

    public async Task<FilteredInterviewQuestionsDto> GetFilteredInterviewQuestionsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var interviewQuestionsList = await unitOfWork.InterviewQuestions.GetAllInterviewQuestions();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = interviewQuestionsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = interviewQuestionsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var interviewQuestionsDto = new List<InterviewQuestionsDto>();
            foreach (var interviewQuestions in paginatedResults)
            {
                interviewQuestionsDto.Add(new InterviewQuestionsDto()
                {
                    Id = interviewQuestions.Id,
                    Category = interviewQuestions.Category,
                    DepartmentId = interviewQuestions.DepartmentId,
                    Question = interviewQuestions.Question,
                    OptionA = interviewQuestions.OptionA,
                    OptionB = interviewQuestions.OptionB,
                    OptionC = interviewQuestions.OptionC,
                    OptionD = interviewQuestions.OptionD,
                    CorrectAnswer = interviewQuestions.CorrectAnswer,
                    CodeSnippets = interviewQuestions.CodeSnippets,
                    AnswerExplanation = interviewQuestions.AnswerExplanation,
                    VideoLink = interviewQuestions.VideoLink,
                    Image = interviewQuestions.Image,
                    Department = mapper.Map<Department,DepartmentDto>(interviewQuestions.Department),
                    CreatedAt = interviewQuestions.CreatedAt,
                    CreatedBy = interviewQuestions.CreatedBy,
                    UpdatedAt = interviewQuestions.UpdatedAt,
                    UpdatedBy = interviewQuestions.UpdatedBy,    
                });
            }
            FilteredInterviewQuestionsDto filteredBudgetsExpensesDto = new()
            {
                InterviewQuestionsDto = interviewQuestionsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (interviewQuestionsList != null)
        {
            IEnumerable<InterviewQuestion> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(interviewQuestionsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(interviewQuestionsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var interviewQuestionss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(interviewQuestionss);
            
            var mappedInterviewQuestions = new List<InterviewQuestionsDto>();

            foreach (var interviewQuestions in paginatedResults)
            {
                
                mappedInterviewQuestions.Add(new InterviewQuestionsDto()
                {
                    Id = interviewQuestions.Id,
                    Category = interviewQuestions.Category,
                    DepartmentId = interviewQuestions.DepartmentId,
                    Question = interviewQuestions.Question,
                    OptionA = interviewQuestions.OptionA,
                    OptionB = interviewQuestions.OptionB,
                    OptionC = interviewQuestions.OptionC,
                    OptionD = interviewQuestions.OptionD,
                    CorrectAnswer = interviewQuestions.CorrectAnswer,
                    CodeSnippets = interviewQuestions.CodeSnippets,
                    AnswerExplanation = interviewQuestions.AnswerExplanation,
                    VideoLink = interviewQuestions.VideoLink,
                    Department = mapper.Map<Department,DepartmentDto>(interviewQuestions.Department),
                    Image = interviewQuestions.Image,
                    CreatedAt = interviewQuestions.CreatedAt,
                    CreatedBy = interviewQuestions.CreatedBy,
                    UpdatedAt = interviewQuestions.UpdatedAt,
                    UpdatedBy = interviewQuestions.UpdatedBy,    
                });
            }
            FilteredInterviewQuestionsDto filteredInterviewQuestionsDto = new()
            {
                InterviewQuestionsDto = mappedInterviewQuestions,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredInterviewQuestionsDto;
        }

        return new FilteredInterviewQuestionsDto();
    }
    private IEnumerable<InterviewQuestion> ApplyFilter(IEnumerable<InterviewQuestion> interviewQuestions, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => interviewQuestions.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => interviewQuestions.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => interviewQuestions.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => interviewQuestions.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var interviewQuestionsValue) => ApplyNumericFilter(interviewQuestions, column, interviewQuestionsValue, operatorType),
            _ => interviewQuestions
        };
    }

    private IEnumerable<InterviewQuestion> ApplyNumericFilter(IEnumerable<InterviewQuestion> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var interviewQuestionsValue) && interviewQuestionsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var interviewQuestionsValue) && interviewQuestionsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var interviewQuestionsValue) && interviewQuestionsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var interviewQuestionsValue) && interviewQuestionsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var interviewQuestionsValue) && interviewQuestionsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var interviewQuestionsValue) && interviewQuestionsValue < value),
        _ => policys
    };
}


    public Task<List<InterviewQuestionsDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<InterviewQuestion> enumerable = unitOfWork.InterviewQuestions.GetAllInterviewQuestions().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var interviewQuestions = enumerable.Select(interviewQuestions => new InterviewQuestionsDto()
            {
                Id = interviewQuestions.Id,
                Category = interviewQuestions.Category,
                DepartmentId = interviewQuestions.DepartmentId,
                Question = interviewQuestions.Question,
                OptionA = interviewQuestions.OptionA,
                OptionB = interviewQuestions.OptionB,
                OptionC = interviewQuestions.OptionC,
                OptionD = interviewQuestions.OptionD,
                CorrectAnswer = interviewQuestions.CorrectAnswer,
                CodeSnippets = interviewQuestions.CodeSnippets,
                AnswerExplanation = interviewQuestions.AnswerExplanation,
                VideoLink = interviewQuestions.VideoLink,
                Image = interviewQuestions.Image,
                Department = mapper.Map<Department,DepartmentDto>(interviewQuestions.Department),
                CreatedAt = interviewQuestions.CreatedAt,
                CreatedBy = interviewQuestions.CreatedBy,
                UpdatedAt = interviewQuestions.UpdatedAt,
                UpdatedBy = interviewQuestions.UpdatedBy,    
            });
            return Task.FromResult(interviewQuestions.ToList());
        }

        var  queryable = unitOfWork.InterviewQuestions.GlobalSearch(searchKey);
        var interviewQuestionss = queryable.Select(interviewQuestions => new InterviewQuestionsDto()
        {
            Id = interviewQuestions.Id,
            Category = interviewQuestions.Category,
            DepartmentId = interviewQuestions.DepartmentId,
            Question = interviewQuestions.Question,
            OptionA = interviewQuestions.OptionA,
            OptionB = interviewQuestions.OptionB,
            OptionC = interviewQuestions.OptionC,
            OptionD = interviewQuestions.OptionD,
            CorrectAnswer = interviewQuestions.CorrectAnswer,
            CodeSnippets = interviewQuestions.CodeSnippets,
            AnswerExplanation = interviewQuestions.AnswerExplanation,
            VideoLink = interviewQuestions.VideoLink,
            Image = interviewQuestions.Image,
            Department = mapper.Map<Department,DepartmentDto>(interviewQuestions.Department),
            CreatedAt = interviewQuestions.CreatedAt,
            CreatedBy = interviewQuestions.CreatedBy,
            UpdatedAt = interviewQuestions.UpdatedAt,
            UpdatedBy = interviewQuestions.UpdatedBy,    
        });
        return Task.FromResult(interviewQuestionss.ToList());
    }

}
