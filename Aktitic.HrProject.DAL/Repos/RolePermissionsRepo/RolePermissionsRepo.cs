using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class RolePermissionsRepo(HrSystemDbContext context) : GenericRepo<RolePermissions>(context), IRolePermissionsRepo
{
    private readonly HrSystemDbContext _context = context;

    public Task DeleteRange(IEnumerable<RolePermissions> rolePermissions)
    {
        _context.RolePermissions?.RemoveRange(rolePermissions);
        _ = _context.SaveChangesAsync();
        return Task.CompletedTask;
    }
}
