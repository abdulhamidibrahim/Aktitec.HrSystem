using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IPayrollDeductionManager
{
    public Task<int> Add(PayrollDeductionAddDto payrollDeductionAddDto);
    public Task<int> Update(PayrollDeductionUpdateDto payrollDeductionUpdateDto, int id);
    public Task<int> Delete(int id);
    public PayrollDeductionReadDto? Get(int id);
    public Task<List<PayrollDeductionReadDto>> GetAll();
    public Task<FilteredPayrollDeductionsDto> GetFilteredPayrollDeductionsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<PayrollDeductionDto>> GlobalSearch(string searchKey,string? column);
  
}