using Aktitic.HrProject.Api.Configuration;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class CompanyRolesRepo(HrSystemDbContext context) : GenericRepo<CompanyRole>(context), ICompanyRolesRepo
{
    public async Task<IEnumerable<CompanyRole>> GetCompanyRoles(int companyId)
    {
        if (context.CompanyRoles != null)
            return await context.CompanyRoles
                .Include(x=>x.RolePermissions)
                .ThenInclude(x=>x.AppPages)
                .ThenInclude(x=>x.AppSubModule)
                .ThenInclude(x=>x.AppModule)
                .Where(x => x.CompanyId == companyId)
                .ToListAsync();
        return [];
    }

    public Task<CompanyRole> GetRole(int roleId)
    {
        if (context.CompanyRoles != null)
            return context.CompanyRoles
                .Include(x => x.RolePermissions)
                .ThenInclude(x => x.AppPages)
                .ThenInclude(x => x.AppSubModule)
                .ThenInclude(x => x.AppModule)
                .FirstOrDefaultAsync(x => x.Id == roleId);
        return Task.FromResult(new CompanyRole());
    }
    

    public async Task<int> CreateRole(CompanyRole companyRole)
    {
        if (context.CompanyRoles != null)
        {
            var entity = await context.CompanyRoles.AddAsync(companyRole);
            await context.SaveChangesAsync();
            return entity.Entity.Id;
        }

        return 0;
    }

    public async Task<ApiRespone<string>> DeleteRole(int roleId)
    {
        if (context.CompanyRoles != null)
        {
            // check that the role is not assigned to any user 
            var isRoleAssigned = await context.Users.AnyAsync(u => u.RoleId == roleId);
            if (isRoleAssigned) return new ApiRespone<string>("Couldn't Delete the Role as is assigned to user(s)")
            {
                Success = false
            };
            var role = await context.CompanyRoles.FindAsync(roleId);
            
            if (role != null)
            {
                context.CompanyRoles.Remove(role);

                return new ApiRespone<string>("Role Deleted Successfully")
                {
                    Success = true
                };
            }
        }

        return new ApiRespone<string>("Role Not Found")
        {
            Success = false
        };
        
    }

    // public void UpdateRole(CompanyRole companyRole)
    // {
    //     if (context.CompanyRoles != null)
    //         context.CompanyRoles.Update(companyRole);
    // }

    public void AssignCompanyRoles(List<CompanyRole> companyRole)
    {
        context.CompanyRoles?.AddRange(companyRole);    
    }

    
}
