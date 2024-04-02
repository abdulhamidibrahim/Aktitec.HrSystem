
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.InvoiceRepo;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class InvoiceManager:IInvoiceManager
{
    private readonly IInvoiceRepo _invoiceRepo;
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public InvoiceManager(IInvoiceRepo invoiceRepo, IUnitOfWork unitOfWork, IMapper mapper, IItemRepo itemRepo)
    {
        _invoiceRepo = invoiceRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _itemRepo = itemRepo;
    }
    
    public Task<int> Add(InvoiceAddDto invoiceAddDto)
    {
        var invoice = new Invoice()
        {
            Email = invoiceAddDto.Email,
            ClientAddress = invoiceAddDto.ClientAddress,
            BillingAddress = invoiceAddDto.BillingAddress,
            InvoiceDate = invoiceAddDto.InvoiceDate,
            DueDate = invoiceAddDto.DueDate,
            OtherInformation = invoiceAddDto.OtherInformation,
            Status = invoiceAddDto.Status,
            InvoiceNumber = invoiceAddDto.InvoiceNumber,
            TotalAmount = invoiceAddDto.TotalAmount,
            Discount = invoiceAddDto.Discount,
            Tax = invoiceAddDto.Tax,
            GrandTotal = invoiceAddDto.GrandTotal,
            Items = _mapper.Map<IEnumerable<ItemDto>,IEnumerable<Item>>
                (invoiceAddDto.Items ?? Array.Empty<ItemDto>()),
            ClientId = invoiceAddDto.ClientId,
            ProjectId = invoiceAddDto.ProjectId,
            
        }; 
        _invoiceRepo.Add(invoice);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(InvoiceUpdateDto invoiceUpdateDto, int id)
    {
        var invoice = _invoiceRepo.GetInvoiceWithItems(id);
        if(invoiceUpdateDto.Items != null)
        {
            var items = _itemRepo.GetItemsByInvoiceId(id);
            foreach (var item in items)
            {
                _itemRepo.Delete(item);
            }

            if (invoice != null)
                invoice.Items = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<Item>>(invoiceUpdateDto.Items);
        }
        
        if (invoice == null) return Task.FromResult(0);
        
        if(invoiceUpdateDto.Email != null) invoice.Email = invoiceUpdateDto.Email;
        if(invoiceUpdateDto.ClientAddress != null) invoice.ClientAddress = invoiceUpdateDto.ClientAddress;
        if(invoiceUpdateDto.BillingAddress != null) invoice.BillingAddress = invoiceUpdateDto.BillingAddress;
        if(invoiceUpdateDto.InvoiceDate != null) invoice.InvoiceDate = invoiceUpdateDto.InvoiceDate;
        if(invoiceUpdateDto.DueDate != null) invoice.DueDate = invoiceUpdateDto.DueDate;
        if(invoiceUpdateDto.Status != null) invoice.Status = invoiceUpdateDto.Status;
        if(invoiceUpdateDto.OtherInformation != null) invoice.OtherInformation = invoiceUpdateDto.OtherInformation;
        if(invoiceUpdateDto.InvoiceNumber != null) invoice.InvoiceNumber = invoiceUpdateDto.InvoiceNumber;
        if(invoiceUpdateDto.TotalAmount != null) invoice.TotalAmount = invoiceUpdateDto.TotalAmount;
        if(invoiceUpdateDto.Discount != null) invoice.Discount = invoiceUpdateDto.Discount;
        if(invoiceUpdateDto.Tax != null) invoice.Tax = invoiceUpdateDto.Tax;
        if(invoiceUpdateDto.GrandTotal != null) invoice.GrandTotal = invoiceUpdateDto.GrandTotal;
        if(invoiceUpdateDto.ClientId != null) invoice.ClientId = invoiceUpdateDto.ClientId;
        if(invoiceUpdateDto.ProjectId != null) invoice.ProjectId = invoiceUpdateDto.ProjectId;
        
        _invoiceRepo.Update(invoice);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _invoiceRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<InvoiceReadDto>? Get(int id)
    {
        var invoice = _invoiceRepo.GetInvoiceWithItems(id);
        if (invoice == null) return null;
        return Task.FromResult(new InvoiceReadDto()
        {
            Id = invoice.Id,
            Email = invoice.Email,
            ClientAddress = invoice.ClientAddress,
            BillingAddress = invoice.BillingAddress,
            InvoiceDate = invoice.InvoiceDate,
            DueDate = invoice.DueDate,
            OtherInformation = invoice.OtherInformation,
            Status = invoice.Status,
            InvoiceNumber = invoice.InvoiceNumber,
            TotalAmount = invoice.TotalAmount,
            Discount = invoice.Discount,
            Tax = invoice.Tax,
            GrandTotal = invoice.GrandTotal,
            Items = _mapper.Map<IEnumerable<Item>,IEnumerable<ItemDto>>(invoice.Items),
            ClientId = invoice.ClientId,
            ProjectId = invoice.ProjectId,
        });
    }

    public Task<List<InvoiceReadDto>> GetAll()
    {
        var invoice = _invoiceRepo.GetAllInvoiceWithItems();
        return Task.FromResult(invoice.Result.Select(i => new InvoiceReadDto()
        {
            Id = i.Id,
            Email = i.Email,
            ClientAddress = i.ClientAddress,
            BillingAddress = i.BillingAddress,
            InvoiceDate = i.InvoiceDate,
            DueDate = i.DueDate,
            OtherInformation = i.OtherInformation,
            Status = i.Status,
            InvoiceNumber = i.InvoiceNumber,
            TotalAmount = i.TotalAmount,
            Discount = i.Discount,
            Tax = i.Tax,
            GrandTotal = i.GrandTotal,
            Items = _mapper.Map<IEnumerable<Item>,IEnumerable<ItemDto>>(i.Items),
            ClientId = i.ClientId,
            ProjectId = i.ProjectId,
            
        }).ToList());
    }
    
     public async Task<FilteredInvoiceDto> GetFilteredInvoicesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _invoiceRepo.GetAllInvoiceWithItems();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedInvoices = new List<InvoiceDto>();
            foreach (var invoice in paginatedResults)
            {
                mappedInvoices.Add(new InvoiceDto()
                {
                    Email = invoice.Email,
                    ClientAddress = invoice.ClientAddress,
                    BillingAddress = invoice.BillingAddress,
                    InvoiceDate = invoice.InvoiceDate,
                    DueDate = invoice.DueDate,
                    OtherInformation = invoice.OtherInformation,
                    Status = invoice.Status,
                    InvoiceNumber = invoice.InvoiceNumber,
                    TotalAmount = invoice.TotalAmount,
                    Discount = invoice.Discount,
                    Tax = invoice.Tax,
                    GrandTotal = invoice.GrandTotal,
                    Items = _mapper.Map<IEnumerable<Item>,IEnumerable<ItemDto>>(invoice.Items),
                    ClientId = invoice.ClientId,
                    ProjectId = invoice.ProjectId,
                });
            }
            FilteredInvoiceDto filteredInvoiceDto = new()
            {
                InvoiceDto = mappedInvoices,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredInvoiceDto;
        }

        if (users != null)
        {
            IEnumerable<Invoice> filteredResults;
        
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

            // var invoices = paginatedResults.ToList();
            // var mappedInvoice = _mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceDto>>(invoices);
            
            var mappedInvoices = new List<InvoiceDto>();

            foreach (var invoice in paginatedResults)
            {
                
                mappedInvoices.Add(new InvoiceDto()
                {
                    Email = invoice.Email,
                    ClientAddress = invoice.ClientAddress,
                    BillingAddress = invoice.BillingAddress,
                    InvoiceDate = invoice.InvoiceDate,
                    DueDate = invoice.DueDate,
                    OtherInformation = invoice.OtherInformation,
                    Status = invoice.Status,
                    InvoiceNumber = invoice.InvoiceNumber,
                    TotalAmount = invoice.TotalAmount,
                    Discount = invoice.Discount,
                    Tax = invoice.Tax,
                    GrandTotal = invoice.GrandTotal,
                    Items = _mapper.Map<IEnumerable<Item>,IEnumerable<ItemDto>>(invoice.Items),
                    ClientId = invoice.ClientId,
                    ProjectId = invoice.ProjectId,
                });
            }
            FilteredInvoiceDto filteredInvoiceDto = new()
            {
                InvoiceDto = mappedInvoices,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredInvoiceDto;
        }

        return new FilteredInvoiceDto();
    }
    private IEnumerable<Invoice> ApplyFilter(IEnumerable<Invoice> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var invoiceValue) => ApplyNumericFilter(users, column, invoiceValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Invoice> ApplyNumericFilter(IEnumerable<Invoice> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue < value),
        _ => users
    };
}


    public Task<List<InvoiceDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Invoice> user;
            user = _invoiceRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var invoice = _mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceDto>>(user);
            return Task.FromResult(invoice.ToList());
        }

        var  users = _invoiceRepo.GlobalSearch(searchKey);
        var invoices = _mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceDto>>(users);
        return Task.FromResult(invoices.ToList());
    }

}
