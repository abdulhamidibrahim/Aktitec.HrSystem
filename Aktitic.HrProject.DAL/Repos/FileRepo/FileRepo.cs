using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.DAL.Repos;

public class FileRepo :GenericRepo<File>,IFileRepo
{
    private readonly HrManagementDbContext _context;

    public FileRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
    // public async Task<UserFile?> GetByEmployeeId(int employeeId)
    // {
    //     var file= await _context.Files.FirstOrDefaultAsync(f => f!.EmployeeId == employeeId);
    //     var emp= _context.Employees.FindAsync(employeeId);
    //    
    //         file!.EmployeeName = emp.Result?.FullName;
    //         return file;
    // }
    
}
