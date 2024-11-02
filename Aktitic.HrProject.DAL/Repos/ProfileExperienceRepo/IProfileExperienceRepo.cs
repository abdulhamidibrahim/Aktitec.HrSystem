using Aktitic.HrProject.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface IProfileExperienceRepo : IGenericRepo<ProfileExperience>
{
    Task<IEnumerable<ProfileExperience>> GetByUserId(int userId);
}