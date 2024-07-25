using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface ISalaryManager
{
    public Task<int> Add(SalaryAddDto policiesAddDto);
    public Task<int> Update(SalaryUpdateDto policiesUpdateDto, int id);
    public Task<int> Delete(int id);
    public SalaryReadDto? Get(int id);
    public Task<List<SalaryReadDto>> GetAll();
    public Task<FilteredSalariesDto> GetFilteredSalariesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<SalaryDto>> GlobalSearch(string searchKey,string? column);
  
}