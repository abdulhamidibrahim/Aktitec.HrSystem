using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public interface IDesignationRepo :IGenericRepo<Designation>
{
    IQueryable<Designation> GlobalSearch(string? searchKey);
    // Task<IQueryable<DesignationDto>> GetDesignationsAndDepartments();
    public IQueryable<Designation> GetDesignationsWithDepartments();

}