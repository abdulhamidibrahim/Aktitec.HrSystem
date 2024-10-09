using Aktitic.HrProject.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface IRolePermissionsRepo :IGenericRepo<RolePermissions>
{
    Task DeleteRange(IEnumerable<RolePermissions> rolePermissions);
}