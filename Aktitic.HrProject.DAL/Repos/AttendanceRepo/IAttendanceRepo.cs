using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public interface IAttendanceRepo :IGenericRepo<Attendance>
{
    IQueryable<Attendance> GlobalSearch(string? searchKey);
    public List<AttendanceRepo.EmployeeAttendanceDalDto> GetEmployeeAttendanceInCurrentMonth();
    
}