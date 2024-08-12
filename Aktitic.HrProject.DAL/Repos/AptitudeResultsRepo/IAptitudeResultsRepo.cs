using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IAptitudeResultsRepo :IGenericRepo<AptitudeResult>
{
    IQueryable<AptitudeResult> GlobalSearch(string? searchKey);
    
    Task<IEnumerable<AptitudeResult>> GetAllAptitudeResults();
}