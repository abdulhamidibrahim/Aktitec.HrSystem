using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IProvidentFundsRepo :IGenericRepo<ProvidentFunds>
{
    IQueryable<ProvidentFunds> GlobalSearch(string? searchKey);
    ProvidentFunds? GetWithEmployees(int id);
    Task<List<ProvidentFunds>> GetAllWithEmployeesAsync();
}