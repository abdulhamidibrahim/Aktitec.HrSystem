using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IPaymentManager
{
    public Task<int> Add(PaymentAddDto paymentAddDto);
    public Task<int> Update(PaymentUpdateDto paymentUpdateDto, int id);
    public Task<int> Delete(int id);
    public PaymentReadDto? Get(int id);
    public Task<List<PaymentReadDto>> GetAll();
    public Task<FilteredPaymentDto> GetFilteredPaymentsAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<PaymentDto>> GlobalSearch(string searchKey,string? column);
  
}