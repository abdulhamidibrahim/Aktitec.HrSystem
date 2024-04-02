using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IEstimateManager
{
    public Task<int> Add(EstimateAddDto estimateAddDto);
    public Task<int> Update(EstimateUpdateDto estimateUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<EstimateReadDto>? Get(int id);
    public Task<List<EstimateReadDto>> GetAll();
    public Task<FilteredEstimateDto> GetFilteredEstimatesAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<EstimateDto>> GlobalSearch(string searchKey,string? column);
  
}