using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class ItemRepo :GenericRepo<Item>,IItemRepo
{
    private readonly HrSystemDbContext _context;

    public ItemRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }


    public List<Item> GetItemsByEstimateId(int estimateId)
    {
        return _context.Items!
            .Where(x => x.EstimateId == estimateId)
            .ToList();
    }

    public IEnumerable<Item> GetItemsByInvoiceId(int id)
    {
        return _context.Items!
            .Where(x => x.Id == id)
            .ToList();
    }
}
