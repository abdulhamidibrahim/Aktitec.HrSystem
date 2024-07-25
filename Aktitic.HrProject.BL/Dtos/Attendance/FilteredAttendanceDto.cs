using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class FilteredAttendanceDto
{
    public List<Dictionary<string,object>> AttendanceDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}

public class TodayFilteredAttendanceDto
{
    public List<Dictionary<string,object>> AttendanceDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int TotalAttendance { get; set; }
}

public class PaginatedAttendanceDto
{
    public List<AttendanceDto> AttendanceDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}