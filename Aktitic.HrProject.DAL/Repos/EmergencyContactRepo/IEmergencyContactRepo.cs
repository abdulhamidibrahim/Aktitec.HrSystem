using Aktitic.HrProject.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface IEmergencyContactRepo : IGenericRepo<EmergencyContact>
{
    Task<EmergencyContact?> GetByUserId(int userId);
}