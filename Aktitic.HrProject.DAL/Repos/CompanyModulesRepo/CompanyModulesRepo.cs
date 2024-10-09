using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class CompanyModulesRepo(HrSystemDbContext context) : GenericRepo<CompanyModule>(context), ICompanyModulesRepo
{
    public async Task<IEnumerable<CompanyModule>> GetCompanyModules(int companyId)
    {
        if (context.CompanyModules != null)
            return await context.CompanyModules
                .Where(x=>x.CompanyId == companyId)
                .Include(x => x.AppModule)
                .Include(x => x.Company)
                .ToListAsync();
        return [];
    }

   
}
