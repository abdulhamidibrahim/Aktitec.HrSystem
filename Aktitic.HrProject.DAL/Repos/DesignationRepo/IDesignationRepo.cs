using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public interface IDesignationRepo :IGenericRepo<Designation>
{
    IQueryable<Designation> GlobalSearch(string? searchKey);

}