using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class PermissionsRepo :GenericRepo<Permission>,IPermissionsRepo
{
    private readonly HrSystemDbContext _context;

    public PermissionsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Permission> GetByClientId(int clientId)
    {
        return _context.Permissions.Where(x => x.ClientId == clientId).ToList();
    }

    public void DeleteRange(List<Permission>? clientPermissions)
    {
        _context.Permissions?.RemoveRange(clientPermissions);
        _context.SaveChanges();
    }

    public void AddRange(List<Permission> permissionDto)
    {
        _context.Permissions?.AddRange(permissionDto);
        _context.SaveChanges();
    }
}
