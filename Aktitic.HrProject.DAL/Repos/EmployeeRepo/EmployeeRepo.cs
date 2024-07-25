using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.EmployeeRepo;

public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
{
    private readonly HrSystemDbContext _context;

    public EmployeeRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<PagedEmployeeResult> GetEmployeesAsync(string? term, string? sort, int page, int limit)
    {
        if (_context.Employees != null)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Department != null &&
                        x.JobPosition != null &&
                        x.Phone != null &&
                        x.Email != null &&
                        x.Department.Name != null &&
                        x.FullName != null &&
                        x.Gender != null &&
                        (x.FullName.ToLower().Contains(term) ||
                         x.Email.ToLower().Contains(term) ||
                         x.Phone.ToLower().Contains(term) ||
                         x.Department.Name.ToLower().Contains(term) ||
                         x.JobPosition.ToLower().Contains(term) ||
                         x.Gender.ToLower().Contains(term)));
            }

            if (!string.IsNullOrWhiteSpace(sort))
            {
                // var sortOrder = sort.StartsWith("desc") ? "desc" : "asc";
                var sortFields = sort.Split(",");
                StringBuilder orderQueryBuilder = new();
                PropertyInfo[] prop = typeof(Employee).GetProperties();
                foreach (var sortField in sortFields)
                {
                    var sortOrder = sortField.StartsWith("-") ? "descending" : "ascending";
                    var sortFieldWithoutOrder = sortField.TrimStart('-');
                    var property = prop.FirstOrDefault(p =>
                        p.Name.Equals(sortFieldWithoutOrder, StringComparison.OrdinalIgnoreCase));
                    if (property == null) continue;
                    orderQueryBuilder.Append($"{property.Name} {sortOrder}, ");
                }

                var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
                query = query.OrderBy(orderQuery);
                
            }

            // pagination
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / limit);
            var employees = query.Skip((page - 1) * limit).Take(limit).ToList();

            return Task.FromResult(new PagedEmployeeResult
            {
                Employees = employees.Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    ImgUrl = x.ImgUrl,
                    Email = x.Email,
                    JobPosition = x.JobPosition,
                    Phone = x.Phone,
                    Age = x.Age,
                    Gender = x.Gender,
                    JoiningDate = x.JoiningDate,
                    YearsOfExperience = x.YearsOfExperience,
                    Salary = x.Salary,
                    Manager = x.ManagerId != null
                        ? _context.Employees.FirstOrDefault(m => m.Id == x.ManagerId)?.FullName
                        : null,
                    Department = x.Department?.Name
                }),
                TotalCount = totalCount,
                TotalPages = totalPages
            });
        }

        return null;
    }

    public async Task<Employee?> GetByEmail(string email)
    {
        if (_context.Employees != null) return await _context.Employees.FirstOrDefaultAsync(x => x.Email == email);
        return null;
    }

    public Task<Employee?>? GetByManager(int managerId)
    {
        if (_context.Employees != null)
            return Task.FromResult(_context.Employees.FirstOrDefault(x => x.ManagerId == managerId));
        return null;
    }

    public async Task<List<Employee>> GetAllManagersAsync()
    {
        if (_context.Employees != null)
            return await await Task.FromResult(_context.Employees
                .Where(x => x.ManagerId == null 
                            && _context.Employees
                                .Any(e => e.ManagerId == x.Id))
                .ToListAsync());
        return new List<Employee>();
    }

    public IQueryable<Employee> GlobalSearch(string? searchKey)
    {
        if (_context.Employees != null)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x => 
                                x.FullName!.ToLower().Contains(searchKey) ||
                                x.Email!.ToLower().Contains(searchKey) ||
                                x.Phone!.ToLower().Contains(searchKey) ||
                                x.Department!.Name!.ToLower().Contains(searchKey) ||
                                x.JobPosition!.ToLower().Contains(searchKey) ||
                                x.Gender!.ToLower().Contains(searchKey));
                return query;
            }
            // if (_context.Employees != null)
            // {
            //     if (column == null) return Task.FromResult(_context.Employees.ToList());
            //     PropertyInfo? propertyInfo = typeof(Employee).GetProperty(column);
            //     if (propertyInfo != null)
            //         return Task.FromResult(_context.Employees.Where(x =>
            //             (bool)x.GetType().GetProperty(column)!.GetValue(x)!.ToString()!.Contains(column)).ToList());
            // }
            //
            // return null;
        }

        return _context.Employees!.AsQueryable();
    }
    

    public async Task<List<Employee>> GetSubordinatesAsync(int employeeId)
    {
        var query = _context.Employees?.AsQueryable()
            .Where(x => x.ManagerId == employeeId)
            .ToListAsync();

        if (query != null) return await query;
        return null;
    }
    
    // get employee with department
    public async Task<List<Employee>> GetEmployeeWithDepartment()
    {
        if (_context.Employees != null)
            return await _context.Employees
                .Include(x => x.Department)
                .ToListAsync();
        return new List<Employee>();
    }

    public async Task<List<Employee>> GetEmployeesByIdsAsync(List<int> employeeIds)
    {
        return await _context.Employees?.Where(x => employeeIds.Contains(x.Id)).ToListAsync()!;
    }

    public async Task<List<Employee>> GetEmployeesWithAttendancesAsync()
    {
        if (_context.Employees != null)
            return await _context.Employees
                                        .Include(x => x.Attendances)
                                        .Include(x=>x.Department)
                                        .ToListAsync();
        
        return (new List<Employee>());
    }


    public async Task<Employee> GetEmployeeWithAttendancesAsync(int id)
    {
        if (_context.Employees != null)
            return  await _context.Employees
                .Include(x => x.Attendances)
                .FirstOrDefaultAsync(x => x.Id == id) ?? new Employee();
        
        return (new Employee());
    }
}

