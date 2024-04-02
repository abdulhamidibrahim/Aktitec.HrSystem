
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class PaymentManager:IPaymentManager
{
    private readonly IPaymentRepo _paymentRepo;
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentManager(IPaymentRepo paymentRepo, IUnitOfWork unitOfWork, IMapper mapper, IItemRepo itemRepo)
    {
        _paymentRepo = paymentRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _itemRepo = itemRepo;
    }
    
    public Task<int> Add(PaymentAddDto paymentAddDto)
    {
        var payment = new Payment()
        {
            InvoiceNumber = paymentAddDto.InvoiceNumber,
            PaidDate = paymentAddDto.PaidDate,
            PaidAmount = paymentAddDto.PaidAmount,
            TotalAmount = paymentAddDto.TotalAmount,
            BankName = paymentAddDto.BankName,
            PaymentType = paymentAddDto.PaymentType,
            Address = paymentAddDto.Address,
            Country = paymentAddDto.Country,
            City = paymentAddDto.City,
            Iban = paymentAddDto.Iban,
            SwiftCode = paymentAddDto.SwiftCode,
            Status = paymentAddDto.Status,
            ClientId = paymentAddDto.ClientId,
            InvoiceId = paymentAddDto.InvoiceId
        }; 
        _paymentRepo.Add(payment);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(PaymentUpdateDto paymentUpdateDto, int id)
    {
        var payment = _paymentRepo.GetPaymentWithClient(id);
        
        
        if (payment == null) return Task.FromResult(0);
        
        if(paymentUpdateDto.InvoiceNumber != null) payment.InvoiceNumber = paymentUpdateDto.InvoiceNumber;
        if(paymentUpdateDto.PaidDate != null) payment.PaidDate = paymentUpdateDto.PaidDate;
        if(paymentUpdateDto.PaidAmount != null) payment.PaidAmount = paymentUpdateDto.PaidAmount;
        if(paymentUpdateDto.TotalAmount != null) payment.TotalAmount = paymentUpdateDto.TotalAmount;
        if(paymentUpdateDto.BankName != null) payment.BankName = paymentUpdateDto.BankName;
        if(paymentUpdateDto.Status != null) payment.Status = paymentUpdateDto.Status;
        if(paymentUpdateDto.PaymentType != null) payment.PaymentType = paymentUpdateDto.PaymentType;
        if(paymentUpdateDto.Address != null) payment.Address = paymentUpdateDto.Address;
        if(paymentUpdateDto.TotalAmount != null) payment.TotalAmount = paymentUpdateDto.TotalAmount;
        if(paymentUpdateDto.Country != null) payment.Country = paymentUpdateDto.Country;
        if(paymentUpdateDto.City != null) payment.City = paymentUpdateDto.City;
        if(paymentUpdateDto.Iban != null) payment.Iban = paymentUpdateDto.Iban;
        if(paymentUpdateDto.ClientId != null) payment.ClientId = paymentUpdateDto.ClientId;
        if(paymentUpdateDto.InvoiceId != null) payment.InvoiceId = paymentUpdateDto.InvoiceId;
        if(paymentUpdateDto.SwiftCode != null) payment.SwiftCode = paymentUpdateDto.SwiftCode;
        
        _paymentRepo.Update(payment);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _paymentRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<PaymentReadDto>? Get(int id)
    {
        var payment = _paymentRepo.GetPaymentWithClient(id);
        if (payment == null) return null;
        return Task.FromResult(new PaymentReadDto()
        {
            Id = payment.Id,
            InvoiceNumber = payment.InvoiceNumber,
            PaidDate = payment.PaidDate,
            PaidAmount = payment.PaidAmount,
            TotalAmount = payment.TotalAmount,
            BankName = payment.BankName,
            PaymentType = payment.PaymentType,
            Address = payment.Address,
            Country = payment.Country,
            City = payment.City,
            Iban = payment.Iban,
            SwiftCode = payment.SwiftCode,
            Status = payment.Status,
            ClientId = payment.ClientId,
            InvoiceId = payment.InvoiceId,
            Client = _mapper.Map<Client,ClientDto>(payment.Client!).FullName,
        });
    }

    public Task<List<PaymentReadDto>> GetAll()
    {
        var payment = _paymentRepo.GetAllPaymentWithClients();
        return Task.FromResult(payment.Result.Select(p => new PaymentReadDto()
        {
            Id = p.Id,
            InvoiceNumber = p.InvoiceNumber,
            PaidDate = p.PaidDate,
            PaidAmount = p.PaidAmount,
            TotalAmount = p.TotalAmount,
            BankName = p.BankName,
            PaymentType = p.PaymentType,
            Address = p.Address,
            Country = p.Country,
            City = p.City,
            Iban = p.Iban,
            SwiftCode = p.SwiftCode,
            Status = p.Status,
            ClientId = p.ClientId,
            InvoiceId = p.InvoiceId,
            Client = _mapper.Map<Client,ClientDto>(p.Client!).FullName

        }).ToList());
    }
    
     public async Task<FilteredPaymentDto> GetFilteredPaymentsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _paymentRepo.GetAllPaymentWithClients();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedPayments = new List<PaymentDto>();
            foreach (var payment in paginatedResults)
            {
                mappedPayments.Add(new PaymentDto()
                {
                    Id = payment.Id,
                    InvoiceNumber = payment.InvoiceNumber,
                    PaidDate = payment.PaidDate,
                    PaidAmount = payment.PaidAmount,
                    TotalAmount = payment.TotalAmount,
                    BankName = payment.BankName,
                    PaymentType = payment.PaymentType,
                    Address = payment.Address,
                    Country = payment.Country,
                    City = payment.City,
                    Iban = payment.Iban,
                    SwiftCode = payment.SwiftCode,
                    Status = payment.Status,
                    ClientId = payment.ClientId,
                    InvoiceId = payment.InvoiceId,
                    Client = _mapper.Map<Client,ClientDto>(payment.Client!).FullName,
                });
            }
            FilteredPaymentDto filteredPaymentDto = new()
            {
                PaymentDto = mappedPayments,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredPaymentDto;
        }

        if (users != null)
        {
            IEnumerable<Payment> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(users, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(users, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var payments = paginatedResults.ToList();
            // var mappedPayment = _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentDto>>(payments);
            
            var mappedPayments = new List<PaymentDto>();

            foreach (var payment in paginatedResults)
            {
                
                mappedPayments.Add(new PaymentDto()
                {
                    Id = payment.Id,
                    InvoiceNumber = payment.InvoiceNumber,
                    PaidDate = payment.PaidDate,
                    PaidAmount = payment.PaidAmount,
                    TotalAmount = payment.TotalAmount,
                    BankName = payment.BankName,
                    PaymentType = payment.PaymentType,
                    Address = payment.Address,
                    Country = payment.Country,
                    City = payment.City,
                    Iban = payment.Iban,
                    SwiftCode = payment.SwiftCode,
                    Status = payment.Status,
                    ClientId = payment.ClientId,
                    InvoiceId = payment.InvoiceId,
                    Client = _mapper.Map<Client,ClientDto>(payment.Client!).FullName,
                });
            }
            FilteredPaymentDto filteredPaymentDto = new()
            {
                PaymentDto = mappedPayments,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredPaymentDto;
        }

        return new FilteredPaymentDto();
    }
    private IEnumerable<Payment> ApplyFilter(IEnumerable<Payment> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var paymentValue) => ApplyNumericFilter(users, column, paymentValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Payment> ApplyNumericFilter(IEnumerable<Payment> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue < value),
        _ => users
    };
}


    public Task<List<PaymentDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Payment> user;
            user = _paymentRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var payment = _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentDto>>(user);
            return Task.FromResult(payment.ToList());
        }

        var  users = _paymentRepo.GlobalSearch(searchKey);
        var payments = _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentDto>>(users);
        return Task.FromResult(payments.ToList());
    }

}
