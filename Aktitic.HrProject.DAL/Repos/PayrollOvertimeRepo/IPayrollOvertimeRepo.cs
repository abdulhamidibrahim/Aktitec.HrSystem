using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IPayrollOvertimeRepo :IGenericRepo<PayrollOvertime>
{
    IQueryable<PayrollOvertime> GlobalSearch(string? searchKey);

}