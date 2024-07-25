using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public class RevenuesRepo :GenericRepo<Revenue>,IRevenuesRepo
{
    private readonly HrSystemDbContext _context;

    public RevenuesRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
   
}
