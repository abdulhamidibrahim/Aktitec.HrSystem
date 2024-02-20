using Aktitic.HrProject.DAL.Models;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.DAL.Repos;

public interface IFileRepo :IGenericRepo<File>
{
    Task<File?> GetByEmployeeId(int employeeId);
}