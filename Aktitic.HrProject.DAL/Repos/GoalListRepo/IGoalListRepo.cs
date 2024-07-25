using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IGoalListRepo :IGenericRepo<GoalList>
{
    IQueryable<GoalList> GlobalSearch(string? searchKey);
    IQueryable<GoalList> GetWithGoalType(int id);
    
    IQueryable<GoalList> GetAllWithGoalType();
    
}