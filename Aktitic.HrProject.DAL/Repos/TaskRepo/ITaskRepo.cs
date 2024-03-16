using Aktitic.HrProject.DAL.Models;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITaskRepo :IGenericRepo<Task>
{
    IQueryable<Task> GlobalSearch(string? searchKey);
}