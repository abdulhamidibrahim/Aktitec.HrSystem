using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class PaymentRepo :GenericRepo<Payment>,IPaymentRepo
{
    private readonly HrSystemDbContext _context;

    public PaymentRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Payment> GlobalSearch(string? searchKey)
    {
        if (_context.Payments!= null)
        {
            var query = 
                _context.Payments
                .Include(x=>x.Client)
                .Include(x=>x.Invoice)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.PaidDate == searchDate );
                    return query;
                }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Invoice!.InvoiceNumber!.ToLower().Contains(searchKey) ||
                        x.BankName!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.PaymentType!.ToLower().Contains(searchKey) ||
                        x.Address!.ToLower().Contains(searchKey) ||
                        x.Iban!.ToLower().Contains(searchKey) ||
                        x.SwiftCode!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.PaidAmount!.ToString().Contains(searchKey) ||
                        x.TotalAmount!.ToString().Contains(searchKey) ||
                        x.City!.ToLower().Contains(searchKey) );
                return query;
            }
           
        }

        return _context.Payments!.AsQueryable();
    }

    public Payment GetPaymentWithClient(int id)
    {
        return _context.Payments!
            .Include(x => x.Client)
            .Include(x=>x.Invoice)
            .FirstOrDefault(x => x.Id == id);
    }

    public Task<List<Payment>> GetAllPaymentWithClients()
    {
        return
            _context.Payments!
                .Include(x => x.Client)
                .Include(x=>x.Invoice)
                .ToListAsync();
    }
}
