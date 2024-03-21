using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public interface ITimesheetRepo :IGenericRepo<TimeSheet>
{
    IQueryable<TimeSheet> GlobalSearch(string? searchKey);
    //get timesheet with employee and project
                    
    Task<List<TimeSheet>> GetTimeSheetWithEmployeeAndProject();
}