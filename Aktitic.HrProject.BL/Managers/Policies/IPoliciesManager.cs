using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrTaskList.BL;

public interface IPoliciesManager
{
    public Task<int> Add(PoliciesAddDto policiesAddDto);
    public Task<int> Update(PoliciesUpdateDto policiesUpdateDto, int id);
    public Task<int> Delete(int id);
    public PoliciesReadDto? Get(int id);
    public Task<List<PoliciesReadDto>> GetAll();
    public Task<FilteredPoliciesDto> GetFilteredPoliciesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<PolicyDto>> GlobalSearch(string searchKey,string? column);
  
}