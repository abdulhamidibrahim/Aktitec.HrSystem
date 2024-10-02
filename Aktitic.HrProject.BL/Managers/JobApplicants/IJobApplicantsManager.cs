using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IJobApplicantsManager
{
    public Task<int> Add(JobApplicantsAddDto jobApplicantsAddDto);
    public Task<int> Update(JobApplicantsUpdateDto jobApplicantsUpdateDto, int id);
    public Task<int> Delete(int id);
    public JobApplicantsReadDto? Get(int id);
    public Task<List<JobApplicantsReadDto>> GetAll();
    public Task<FilteredJobApplicantsDto> GetFilteredJobApplicantsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<JobApplicantsDto>> GlobalSearch(string searchKey,string? column);
    public Task<object> GetTotalCount();
  
}