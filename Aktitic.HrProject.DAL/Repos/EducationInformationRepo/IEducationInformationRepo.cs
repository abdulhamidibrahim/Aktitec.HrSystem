using Aktitic.HrProject.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface IEducationInformationRepo : IGenericRepo<EducationInformation>
{
    Task<IEnumerable<EducationInformation>> GetByUserId(int userId);
}