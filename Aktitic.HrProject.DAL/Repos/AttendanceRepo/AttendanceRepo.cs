using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class AttendanceRepo :GenericRepo<Attendance>,IAttendanceRepo
{
    private readonly HrSystemDbContext _context;

    public AttendanceRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Attendance> GlobalSearch(string? searchKey)
    {
        if (_context.Attendances != null)
        {
            var query = _context.Attendances.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Date!.ToString().ToLower().Contains(searchKey) ||
                        x.PunchIn!.ToString().ToLower().Contains(searchKey) ||
                        x.PunchOut!.ToString().ToLower().Contains(searchKey) ||
                        x.Production!.ToString().ToLower().Contains(searchKey) ||
                        x.Break!.ToString().ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Attendances!.AsQueryable();
    }
    
    
    public class EmployeeAttendanceDalDto
    {
        public List<AttendanceDto> Attendance { get; set; }
        public int? EmployeeId { get; set; }
        public DateOnly Date { get; set; }
        public bool Attended { get; set; }
    }
    
    public List<EmployeeAttendanceDalDto> GetEmployeeAttendanceInCurrentMonth(List<AttendanceDto> attendanceDtoList)
    {
        // Get the current month and year
        int currentYear = DateTime.Today.Year;
        int currentMonth = DateTime.Today.Month;

        // Define the start and end date of the current month
        DateOnly startDate = new DateOnly(currentYear, currentMonth, 1);
        DateOnly endDate = startDate.AddMonths(1).AddDays(-1);

        // Filter attendance records for the current month
        var filteredAttendance = attendanceDtoList
            .Where(attendance => attendance.Date.HasValue &&
                                 attendance.Date.Value >= startDate &&
                                 attendance.Date.Value <= endDate)
            .ToList();

        // Group attendance records by employee ID
        var groupedByEmployee = filteredAttendance
            .GroupBy(attendance => attendance.EmployeeId)
            .ToList();

        // Create a list to hold attendance information for each employee
        var employeeAttendanceList = new List<EmployeeAttendanceDalDto>();

        foreach (var group in groupedByEmployee)
        {
            var employeeId = group.Key;
            var attendanceList = group.ToList();

            // Create a new EmployeeAttendanceDalDto for each employee
            var employeeAttendance = new EmployeeAttendanceDalDto
            {
                EmployeeId = employeeId,
                Date = startDate,
                Attended = true, // Assuming attendance is recorded for each employee on each date
                Attendance = attendanceList
            };

            employeeAttendanceList.Add(employeeAttendance);
        }

        return employeeAttendanceList;
    }

}
