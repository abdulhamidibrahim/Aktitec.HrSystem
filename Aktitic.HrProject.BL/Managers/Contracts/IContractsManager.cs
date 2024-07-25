using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IContractsManager
{
    public Task<int> Add(ContractAddDto contractAddDto);
    public Task<int> Update(ContractUpdateDto contractUpdateDto, int id);
    public Task<int> Delete(int id);
    public ContractReadDto? Get(int id);
    public Task<List<ContractReadDto>> GetAll();
    public Task<FilteredContractDto> GetFilteredContractsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);
    public Task<List<ContractDto>> GlobalSearch(string searchKey,string? column);
  
}