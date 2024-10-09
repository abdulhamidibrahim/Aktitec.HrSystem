using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class AppModulesRepo(HrSystemDbContext context) : GenericRepo<AppModule>(context), IAppModulesRepo
{
    public async Task<IEnumerable<AppModule>> GetAppModules()
    {
        if (context.AppModules != null)
            return await context.AppModules
                .Include(x => x.AppSubModules)
                .ThenInclude(x => x.AppPages)
                .ToListAsync();
        return [];
    }

    public async Task<AppModule?> GetAppModuleById(int id)
    {
        if (context.AppModules != null)
            return await context.AppModules
                .Include(x => x.AppSubModules)
                .ThenInclude(x => x.AppPages)
                .FirstOrDefaultAsync(x => x.Id == id);
        return new AppModule();
    }
}
