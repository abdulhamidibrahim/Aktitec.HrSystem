using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.InvoiceRepo;

public interface IInvoiceRepo :IGenericRepo<Invoice>
{
    IQueryable<Invoice> GlobalSearch(string? searchKey);
    Task<Invoice> GetInvoiceWithItems(int id);
    Task<List<Invoice>> GetAllInvoiceWithItems();
    
}