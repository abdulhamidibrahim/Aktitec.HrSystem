using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITrainingTypeRepo :IGenericRepo<TrainingType>
{
    IQueryable<TrainingType> GlobalSearch(string? searchKey);
    
}