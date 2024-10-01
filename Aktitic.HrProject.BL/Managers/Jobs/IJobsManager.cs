using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IJobsManager
{
    public Task<int> Add(JobsAddDto jobsAddDto);
    public Task<int> Update(JobsUpdateDto jobsUpdateDto, int id);
    public Task<int> Delete(int id);
    public JobsReadDto? Get(int id);
    public Task<List<JobsReadDto>> GetAll();
    public Task<FilteredJobsDto> GetFilteredJobsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize, string? category);

    public Task<List<JobsDto>> GlobalSearch(string searchKey,string? column);

    Task<object> GetTotalJobs();
}