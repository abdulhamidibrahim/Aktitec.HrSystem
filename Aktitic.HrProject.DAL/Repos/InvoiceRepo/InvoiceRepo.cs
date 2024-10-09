using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos.InvoiceRepo;

public class InvoiceRepo(HrSystemDbContext context) : GenericRepo<Invoice>(context), IInvoiceRepo
{
    private readonly HrSystemDbContext _context = context;

    public IQueryable<Invoice> GlobalSearch(string? searchKey)
    {
        if (_context.Invoices != null)
        {
            var query = 
                _context.Invoices
                .Include(x=>x.Items)
                .Include(x=>x.Client)
                .Include(x=>x.Project)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.InvoiceDate == searchDate ||
                            x.DueDate == searchDate);
                    return query;
                }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Email!.ToLower().Contains(searchKey) ||
                        x.OtherInformation!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.BillingAddress!.ToLower().Contains(searchKey) ||
                        x.ClientAddress!.ToLower().Contains(searchKey) ||
                        x.InvoiceNumber!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Invoices!.AsQueryable();
    }

    public Task<Invoice> GetInvoiceWithItems(int id)
    {
        return _context.Invoices!
            .Include(x => x.Items)
            .Include(x => x.Client)
            .Include(x => x.Project)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Invoice>> GetAllInvoiceWithItems()
    {
        return
            _context.Invoices!
                .Include(x => x.Items)
                .Include(x => x.Client)
                .Include(x => x.Project)
                .ToListAsync();
    }
}
