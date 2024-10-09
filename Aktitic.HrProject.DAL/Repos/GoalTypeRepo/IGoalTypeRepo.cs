using System.Linq;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IGoalTypeRepo :IGenericRepo<GoalType>
{
    IQueryable<GoalType> GlobalSearch(string? searchKey);
    
}