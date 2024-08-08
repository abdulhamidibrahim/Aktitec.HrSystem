using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IInterviewQuestionsManager
{
    public Task<int> Add(InterviewQuestionsAddDto interviewQuestionsAddDto);
    public Task<int> Update(InterviewQuestionsUpdateDto interviewQuestionsUpdateDto, int id);
    public Task<int> Delete(int id);
    public InterviewQuestionsReadDto? Get(int id);
    public Task<List<InterviewQuestionsReadDto>> GetAll();
    public Task<FilteredInterviewQuestionsDto> GetFilteredInterviewQuestionsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<InterviewQuestionsDto>> GlobalSearch(string searchKey,string? column);
  
}