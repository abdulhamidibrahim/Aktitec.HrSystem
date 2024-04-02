using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ILeaveSettingRepo :IGenericRepo<LeaveSettings>
{
    // get leave setting with employee and ApprovedBy
    
    List<Leaves>? GetLeavesWithEmployee();
}