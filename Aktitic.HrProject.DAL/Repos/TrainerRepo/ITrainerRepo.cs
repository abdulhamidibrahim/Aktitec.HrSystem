using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITrainerRepo :IGenericRepo<Trainer>
{
    IQueryable<Trainer> GlobalSearch(string? searchKey);
    
}