using Aktitic.HrProject.DAL.Models;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface IAppModulesRepo :IGenericRepo<AppModule>
{
    Task<IEnumerable<AppModule>> GetAppModules();   
    Task<AppModule?> GetAppModuleById(int id);
}