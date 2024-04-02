using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IPermissionsRepo :IGenericRepo<Permission>
{
    List<Permission> GetByClientId(int clientId);
    void DeleteRange(List<Permission>? clientPermissions);
    void AddRange(List<Permission> permissionDto);
}