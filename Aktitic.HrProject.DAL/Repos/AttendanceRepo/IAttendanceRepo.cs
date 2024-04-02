using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public interface IAttendanceRepo :IGenericRepo<Attendance>
{
    IQueryable<Attendance> GlobalSearch(string? searchKey);
    public List<AttendanceRepo.EmployeeAttendanceDalDto> GetEmployeeAttendanceInCurrentMonth(List<AttendanceDto> attendance);
    List<Attendance> GetByEmployeeId(int employeeId);
    List<Attendance> GetAttendanceWithEmployee();
}