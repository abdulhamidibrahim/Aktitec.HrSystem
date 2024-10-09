using System.Linq;
using System.Threading.Tasks;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ILogsRepo :IGenericRepo<AuditLog>
{
    IQueryable<AuditLog> GlobalSearch(string? searchKey);
    Task<IEnumerable<AuditLog>>  GetAllLogs();
}