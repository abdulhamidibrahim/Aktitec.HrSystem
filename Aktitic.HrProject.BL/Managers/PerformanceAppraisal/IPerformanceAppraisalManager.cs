using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IPerformanceAppraisalManager
{
    public Task<int> Add(PerformanceAppraisalAddDto performanceAppraisalAddDto);
    public Task<int> Update(PerformanceAppraisalUpdateDto performanceAppraisalUpdateDto, int id);
    public Task<int> Delete(int id);
    public PerformanceAppraisalReadDto? Get(int id);
    public Task<List<PerformanceAppraisalReadDto>> GetAll();
    public Task<FilteredPerformanceAppraisalDto> GetFilteredPerformanceAppraisalsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<PerformanceAppraisalDto>> GlobalSearch(string searchKey,string? column);
  
}