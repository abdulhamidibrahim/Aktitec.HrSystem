using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class EmployeeRepo :GenericRepo<Employee>,IEmployeeRepo
{
    private readonly HrManagementDbContext _context;

    public EmployeeRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<PagedEmployeeResult> GetEmployeesAsync(string? term, string? sort, int page, int limit)
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
                       (x.FullName.ToLower().Contains(term) ||
                        x.Email.ToLower().Contains(term) || 
                        x.Phone.ToLower().Contains(term) || 
                        x.Department.Name.ToLower().Contains(term) || 
                        x.JobPosition.ToLower().Contains(term)));
        }
        
        if (!string.IsNullOrWhiteSpace(sort))
        {
            // var sortOrder = sort.StartsWith("desc") ? "desc" : "asc";
            var sortFields = sort.Split(",");
            StringBuilder orderQueryBuilder = new ();
            PropertyInfo[] prop = typeof(Employee).GetProperties();
            foreach (var sortField in sortFields)
            {
                var sortOrder = sortField.StartsWith("-") ? "descending" : "ascending";
                var sortFieldWithoutOrder = sortField.TrimStart('-');
                var property = prop.FirstOrDefault(p => p.Name.Equals( sortFieldWithoutOrder,StringComparison.OrdinalIgnoreCase));
                if (property == null) continue;
                orderQueryBuilder.Append($"{property.Name} {sortOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            query = query.OrderBy(orderQuery);
            
            // foreach (var sortField in sortFields)
            // {
            //     query = query.OrderBy(sortField);
            // }
            // query = query.OrderBy(sort);
        }
        
        // pagination
        var totalCount = query.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / limit);
        var employees = query.Skip((page - 1) * limit).Take(limit).ToList();
        
        return Task.FromResult(new PagedEmployeeResult
        {
            Employees = employees,
            TotalCount = totalCount,
            TotalPages = totalPages
        });

        
    }

    // public Task<Employee> GetFilteredEmployees(string column, string value1, string? value2, string @operator)
    // {
    //     if (column == null) throw new ArgumentNullException(nameof(column));
    //     PropertyInfo? propertyInfo = typeof(Employee).GetProperty(nameof(column));
    //     
    //     string GetValue(String columnName)
    //     {
    //         var property = GetType().GetProperty(columnName);
    //         if (property != null)
    //         {
    //             var value = property.GetValue(this)?.ToString();
    //             return value ?? string.Empty;
    //         }
    //
    //         return string.Empty;
    //     }
    //     
    //     _context.Employees.Where(x=>x.)
    //     return null;
    // }
}

public class Filter
{
    
}
