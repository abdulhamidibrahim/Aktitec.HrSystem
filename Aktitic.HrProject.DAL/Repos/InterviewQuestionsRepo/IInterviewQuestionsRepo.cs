using System.Linq;
using System.Threading.Tasks;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IInterviewQuestionsRepo :IGenericRepo<InterviewQuestion>
{
    IQueryable<InterviewQuestion> GlobalSearch(string? searchKey);
    
    Task<IEnumerable<InterviewQuestion>> GetAllInterviewQuestions();
}