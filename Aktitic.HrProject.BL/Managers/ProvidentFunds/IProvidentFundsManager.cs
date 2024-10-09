using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrTaskList.BL;

public interface IProvidentFundsManager
{
    public Task<int> Add(ProvidentFundsAddDto providentFundsAddDto);
    public Task<int> Update(ProvidentFundsUpdateDto providentFundsUpdateDto, int id);
    public Task<int> Delete(int id);
    public ProvidentFundsReadDto? Get(int id);
    public Task<List<ProvidentFundsReadDto>> GetAll();
    public Task<FilteredProvidentFundsDto> GetFilteredProvidentFundsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<ProvidentFundsDto>> GlobalSearch(string searchKey,string? column);
  
}