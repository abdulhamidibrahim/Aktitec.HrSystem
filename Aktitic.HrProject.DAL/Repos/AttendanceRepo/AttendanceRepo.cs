using System.Linq.Dynamic.Core;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
                if(DateOnly.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate);
                    return query;
                }
                if (DateTime.TryParse(searchKey,out var searchDateValue))
                {
                    query = query
                        .Where(x =>
                            x.PunchIn == searchDateValue ||
                            x.PunchOut == searchDateValue);
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Production!.ToString().ToLower().Contains(searchKey) ||
                        x.Overtime!.ToString().ToLower().Contains(searchKey) ||
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
        // public DateOnly Date { get; set; }
        // public bool Attended { get; set; }
    }
    
    public async Task<List<Attendance>> GetEmployeeAttendanceInCurrentMonth(List<AttendanceDto> attendanceDto)
    {
        int currentYear = DateTime.Today.Year;
        int currentMonth = DateTime.Today.Month;

        DateOnly startDate = new DateOnly(currentYear, currentMonth, 1);
        DateOnly endDate = startDate.AddMonths(1).AddDays(-1);
        
        List<int> employeeIds = attendanceDto.Select(a => a.EmployeeId).ToList();
        
        // Filter attendance records within the current month
        if (_context.Attendances != null)
        {
            var filteredAttendance = await _context.Attendances
                .Include(a=> a.Employee)
                .Where(attendance => employeeIds.Contains(attendance.EmployeeId)&& 
                                     attendance.Date.HasValue &&
                                     attendance.Date.Value >= startDate &&
                                     attendance.Date.Value <= endDate).ToListAsync();
            return filteredAttendance;
        }
        
        return new List<Attendance>();
    }

    public List<Attendance> GetByEmployeeId(int employeeId)
    {
        return _context.Attendances!.Where(x => x.EmployeeId == employeeId).ToList();
    }

    public async Task<List<Attendance>> GetAttendanceWithEmployee()
    {
        return await _context.Attendances!.Include(x => x.Employee).ToListAsync();
    }
}
