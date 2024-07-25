using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TrainerRepo :GenericRepo<Trainer>,ITrainerRepo
{
    private readonly HrSystemDbContext _context;

    public TrainerRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Trainer> GlobalSearch(string? searchKey)
    {
        if (_context.Trainers != null)
        {
            var query = _context.Trainers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                //
                // if( DateOnly.TryParse(searchKey, out var searchDate))
                // {
                //     query = query
                //         .Where(x =>
                //             x.Date == searchDate );
                //     return query;
                // }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.FirstName!.ToLower().Contains(searchKey) ||
                        x.LastName!.ToLower().Contains(searchKey) ||
                        x.Email!.ToLower().Contains(searchKey) ||
                        x.Role!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Trainers!.AsQueryable();
    }
}
