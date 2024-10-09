using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrTaskList.BL;

public interface IPayrollAdditionManager
{
    public Task<int> Add(PayrollAdditionAddDto payrollAdditionAddDto);
    public Task<int> Update(PayrollAdditionUpdateDto payrollAdditionUpdateDto, int id);
    public Task<int> Delete(int id);
    public PayrollAdditionReadDto? Get(int id);
    public Task<List<PayrollAdditionReadDto>> GetAll();
    public Task<FilteredPayrollAdditionsDto> GetFilteredPayrollAdditionsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<PayrollAdditionDto>> GlobalSearch(string searchKey,string? column);
  
}