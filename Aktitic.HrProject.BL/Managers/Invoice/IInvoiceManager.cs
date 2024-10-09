using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrTaskList.BL;

public interface IInvoiceManager
{
    public Task<int> Add(InvoiceAddDto invoiceAddDto);
    public Task<int> Update(InvoiceUpdateDto invoiceUpdateDto, int id);
    public Task<int> Delete(int id);
    public InvoiceReadDto? Get(int id);
    public Task<List<InvoiceReadDto>> GetAll();
    public Task<FilteredInvoiceDto> GetFilteredInvoicesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<InvoiceDto>> GlobalSearch(string searchKey,string? column);
  
}