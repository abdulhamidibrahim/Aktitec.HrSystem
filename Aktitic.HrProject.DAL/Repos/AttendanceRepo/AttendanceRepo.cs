using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class AttendanceRepo :GenericRepo<Attendance>,IAttendanceRepo
{
    private readonly HrManagementDbContext _context;

    public AttendanceRepo(HrManagementDbContext context) : base(context)
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
        public int? EmployeeId { get; set; }
        public DateOnly Date { get; set; }
        public bool Attended { get; set; }
    }
    
        public List<EmployeeAttendanceDalDto> GetEmployeeAttendanceInCurrentMonth()
        {
            // Get the current month and year
            int currentYear = DateTime.Today.Year;
            int currentMonth = DateTime.Today.Month;

            // Define the start and end date of the current month
            DateOnly startDate = new DateOnly(currentYear, currentMonth, 1);
            DateOnly endDate = startDate.AddMonths(1).AddDays(-1); // Last day of the current month

            // Query attendance records for the current month
            var attendanceQuery =
                from attendance in _context.Attendances
                where attendance.Date.HasValue && attendance.Date.Value >= startDate && attendance.Date.Value <= endDate
                select new EmployeeAttendanceDalDto
                {
                    EmployeeId = attendance.EmployeeId,
                    Date = attendance.Date.Value,
                    Attended = true // Assuming attendance is recorded for each employee on each date
                };

            // Create a list to hold attendance information for each employee
            return attendanceQuery.ToList();
        }
    }
