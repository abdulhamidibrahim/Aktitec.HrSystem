using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface IAttendanceManager
{
    public Task<int> Add(AttendanceAddDto attendanceAddDto);
    public Task<int> Update(AttendanceUpdateDto attendanceUpdateDto, int id);
    public Task<int> Delete(int id);
    public AttendanceReadDto? Get(int id);
    public List<AttendanceReadDto> GetAll();
    public Task<FilteredAttendanceDto> GetFilteredAttendancesAsync
        (string? column, string? value1, string? operator1, string? value2,
            string? operator2 , int page, int pageSize= 1);

    public Task<TodayFilteredAttendanceDto> GetTodayFilteredAttendancesAsync
    (string? column, string? value1, string? operator1, string? value2,
        string? operator2, int page, int pageSize = 1);

    public Task<List<AttendanceDto>> GlobalSearch(string searchKey,string? column);
    // public Task<List<AttendanceInMonthDto>> GetAllEmployeeAttendanceInCurrentMonth(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);
    public Task<List<Dictionary<string, object>>> GetAllEmployeeAttendanceInCurrentMonth(List<Employee> attendances);

    public Task<List<AttendanceDto>> GetByEmployeeId(int employeeId);
    // public Task<List<Attendance>> GetAttendanceWithEmployee();
    public Task<PaginatedAttendanceDto> GetEmployeeAttendance(int employeeId, int page, int pageSize);
}