using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ICandidatesRepo :IGenericRepo<Candidate>
{
    IQueryable<Candidate> GlobalSearch(string? searchKey);
    
    Task<IEnumerable<Candidate>> GetAllCandidates();
}