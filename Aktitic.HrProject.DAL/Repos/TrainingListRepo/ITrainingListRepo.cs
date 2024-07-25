using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITrainingListRepo :IGenericRepo<TrainingList>
{
    IQueryable<TrainingList> GlobalSearch(string? searchKey);
    IQueryable<TrainingList> GetWithEmployeeAndTrainer(int id);
    
    IQueryable<TrainingList> GetAllWithEmployeeAndTrainer();
    
}