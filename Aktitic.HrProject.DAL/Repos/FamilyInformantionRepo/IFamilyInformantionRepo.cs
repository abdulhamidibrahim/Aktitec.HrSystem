using Aktitic.HrProject.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface IFamilyInformantionRepo : IGenericRepo<FamilyInformation>
{
    Task<IEnumerable<FamilyInformation>> GetByUserId(int userId);
}