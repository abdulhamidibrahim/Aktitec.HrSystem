
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
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(PaymentAddDto paymentAddDto)
    {
        var payment = new Payment()
        {
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
            InvoiceId = paymentAddDto.InvoiceId,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Payment.Add(payment);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(PaymentUpdateDto paymentUpdateDto, int id)
    {
        var payment = _unitOfWork.Payment.GetPaymentWithClient(id);
        
        
        if (payment == null) return Task.FromResult(0);
        
        // if(paymentUpdateDto.InvoiceNumber != null) payment.InvoiceNumber = paymentUpdateDto.InvoiceNumber;
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

        payment.UpdatedAt = DateTime.Now;
        _unitOfWork.Payment.Update(payment);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var payment = _unitOfWork.Payment.GetById(id);
        if (payment==null) return Task.FromResult(0);
        payment.IsDeleted = true;
        payment.DeletedAt = DateTime.Now;
        _unitOfWork.Payment.Update(payment);
        return _unitOfWork.SaveChangesAsync();
    }

    public PaymentReadDto? Get(int id)
    {
        var payment = _unitOfWork.Payment.GetPaymentWithClient(id);
        if (payment == null) return null;
        return new PaymentReadDto()
        {
            Id = payment.Id,
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
            Client = _mapper.Map<Client,ClientDto>(payment.Client!)?.FullName,
            InvoiceNumber = _mapper.Map<Invoice,InvoiceDto>(payment.Invoice!)?.InvoiceNumber,
            
        };
    }

    public Task<List<PaymentReadDto>> GetAll()
    {
        var payment = _unitOfWork.Payment.GetAllPaymentWithClients();
        return Task.FromResult(payment.Result.Select(p => new PaymentReadDto()
        {
            Id = p.Id,
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
            Client = _mapper.Map<Client,ClientDto>(p.Client!)?.FullName,
            InvoiceNumber = _mapper.Map<Invoice,InvoiceDto>(p.Invoice!)?.InvoiceNumber,

        }).ToList());
    }
    
     public async Task<FilteredPaymentDto> GetFilteredPaymentsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var payments = await _unitOfWork.Payment.GetAllPaymentWithClients();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = payments.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var paymentList = payments.ToList();

            var paginatedResults = paymentList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedPayments = new List<PaymentDto>();
            foreach (var payment in paginatedResults)
            {
                mappedPayments.Add(new PaymentDto()
                {
                    Id = payment.Id,
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
                    Client = _mapper.Map<Client,ClientDto>(payment.Client!)?.FullName,
                    InvoiceNumber = _mapper.Map<Invoice,InvoiceDto>(payment.Invoice!)?.InvoiceNumber,
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

        if (payments != null)
        {
            IEnumerable<Payment> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(payments, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(payments, column, value2, operator2));
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
                    Client = _mapper.Map<Client,ClientDto>(payment.Client!)?.FullName,
                    InvoiceNumber = _mapper.Map<Invoice,InvoiceDto>(payment.Invoice!)?.InvoiceNumber,
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
    private IEnumerable<Payment> ApplyFilter(IEnumerable<Payment> payments, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => payments.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => payments.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => payments.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => payments.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var paymentValue) => ApplyNumericFilter(payments, column, paymentValue, operatorType),
            _ => payments
        };
    }

    private IEnumerable<Payment> ApplyNumericFilter(IEnumerable<Payment> payments, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => payments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue == value),
        "neq" => payments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue != value),
        "gte" => payments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue >= value),
        "gt" => payments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue > value),
        "lte" => payments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue <= value),
        "lt" => payments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var paymentValue) && paymentValue < value),
        _ => payments
    };
}


    public Task<List<PaymentDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Payment> paymentDto;
            paymentDto = _unitOfWork.Payment.GetAllPaymentWithClients().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var pays = new List<PaymentDto>();
            foreach (var payment in paymentDto)
            {
                
                pays.Add(new PaymentDto()
                {
                    Id = payment.Id,
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
                    Client = _mapper.Map<Client,ClientDto>(payment.Client!)?.FullName,
                    InvoiceNumber = _mapper.Map<Invoice,InvoiceDto>(payment.Invoice!)?.InvoiceNumber,
                });
            }           
            return Task.FromResult(pays.ToList());
        }

        var  paymentDtos = _unitOfWork.Payment.GlobalSearch(searchKey);
        var payments = new List<PaymentDto>();
        foreach (var payment in paymentDtos)
        {
                
            payments.Add(new PaymentDto()
            {
                Id = payment.Id,
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
                Client = _mapper.Map<Client,ClientDto>(payment.Client!)?.FullName,
                InvoiceNumber = _mapper.Map<Invoice,InvoiceDto>(payment.Invoice!)?.InvoiceNumber,
            });
        }           
        return Task.FromResult(payments.ToList());
    }

}
