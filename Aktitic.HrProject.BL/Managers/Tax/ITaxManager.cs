using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface ITaxManager
{
    public Task<int> Add(TaxAddDto taxAddDto);
    public Task<int> Update(TaxUpdateDto taxUpdateDto, int id);
    public Task<int> Delete(int id);
    public TaxReadDto? Get(int id);
    public Task<List<TaxReadDto>> GetAll();
    public Task<FilteredTaxDto> GetFilteredTaxsAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<TaxDto>> GlobalSearch(string searchKey,string? column);
  
}