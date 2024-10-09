using Aktitic.HrProject.DAL.Models;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface ICompanyModulesRepo :IGenericRepo<CompanyModule>
{
    Task<IEnumerable<CompanyModule>> GetCompanyModules(int companyId);   
    // Task<AppModule?> GetAppModuleById(int id);
}