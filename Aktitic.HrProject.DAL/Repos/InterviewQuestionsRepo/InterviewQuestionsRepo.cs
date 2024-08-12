using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class InterviewQuestionsRepo :GenericRepo<InterviewQuestion>,IInterviewQuestionsRepo
{
    private readonly HrSystemDbContext _context;

    public InterviewQuestionsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<InterviewQuestion> GlobalSearch(string? searchKey)
    {
        if (_context.InterviewQuestions != null)
        {
            var query = _context.InterviewQuestions
                .Include(x=>x.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if(query.Any(x => x.Department.Name != null && x.Department.Name.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Department.Name != null && x.Department.Name.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.Category!.ToLower().Contains(searchKey) ||
                        x.Question!.ToLower().Contains(searchKey) ||
                        x.OptionA!.ToLower().Contains(searchKey) ||
                        x.OptionB!.ToLower().Contains(searchKey) ||
                        x.OptionC!.ToLower().Contains(searchKey) ||
                        x.OptionD!.ToLower().Contains(searchKey) ||
                        x.AnswerExplanation!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.InterviewQuestions!.AsQueryable();
    }

    public async Task<IEnumerable<InterviewQuestion>> GetAllInterviewQuestions()
    {
        return await _context.InterviewQuestions!
            .Include(x=>x.Department)
            .ToListAsync();
    }
}
