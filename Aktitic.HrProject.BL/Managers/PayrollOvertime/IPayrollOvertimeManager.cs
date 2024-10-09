using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrTaskList.BL;

public interface IPayrollOvertimeManager
{
    public Task<int> Add(PayrollOvertimeAddDto payrollOvertimeAddDto);
    public Task<int> Update(PayrollOvertimeUpdateDto payrollOvertimeUpdateDto, int id);
    public Task<int> Delete(int id);
    public PayrollOvertimeReadDto? Get(int id);
    public Task<List<PayrollOvertimeReadDto>> GetAll();
    public Task<FilteredPayrollOvertimesDto> GetFilteredPayrollOvertimesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<PayrollOvertimeDto>> GlobalSearch(string searchKey,string? column);
  
}