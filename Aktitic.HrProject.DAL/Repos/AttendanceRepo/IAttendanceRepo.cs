using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public interface IAttendanceRepo :IGenericRepo<Attendance>
{
    IQueryable<Attendance> GlobalSearch(string? searchKey);

    public Task<List<Attendance>> GetEmployeeAttendanceInCurrentMonth(List<AttendanceDto> attendanceDto);
    List<Attendance> GetByEmployeeId(int employeeId);
    Task<List<Attendance>> GetAttendanceWithEmployee();
}