using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos.TrainingListRepo;

public class TrainingListRepo(HrSystemDbContext context) : GenericRepo<TrainingList>(context), ITrainingListRepo
{
    private readonly HrSystemDbContext _context = context;

    public IQueryable<TrainingList> GlobalSearch(string? searchKey)
    {
        if (_context.TrainingLists != null)
        {
            var query = _context.TrainingLists
                .Include(x=>x.TrainingType)
                .Include(x=>x.Trainer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.StartDate == searchDate ||
                            x.EndDate == searchDate );
                    return query;
                }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Cost!.ToString().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.TrainingLists!.AsQueryable();
    }

    public IQueryable<TrainingList> GetWithEmployeeAndTrainer(int id)
    {
        
        return _context.TrainingLists!.Where(x => x.Id == id)
            .Include(x => x.Employee)
            .Include(x => x.Trainer)
            .Include(x => x.TrainingType);
    }

    public IQueryable<TrainingList> GetAllWithEmployeeAndTrainer()
    {
        return _context.TrainingLists!
            .Include(x => x.Employee)
            .Include(x => x.Trainer)
            .Include(x=> x.TrainingType);
    }
}
