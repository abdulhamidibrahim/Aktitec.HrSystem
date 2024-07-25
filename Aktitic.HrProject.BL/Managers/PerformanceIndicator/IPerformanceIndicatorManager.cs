using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IPerformanceIndicatorManager
{
    public Task<int> Add(PerformanceIndicatorAddDto performanceIndicatorAddDto);
    public Task<int> Update(PerformanceIndicatorUpdateDto performanceIndicatorUpdateDto, int id);
    public Task<int> Delete(int id);
    public PerformanceIndicatorReadSingleDto? Get(int id);
    public Task<List<PerformanceIndicatorReadDto>> GetAll();
    public Task<FilteredPerformanceIndicatorDto> GetFilteredPerformanceIndicatorsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<PerformanceIndicatorDto>> GlobalSearch(string searchKey,string? column);
  
}