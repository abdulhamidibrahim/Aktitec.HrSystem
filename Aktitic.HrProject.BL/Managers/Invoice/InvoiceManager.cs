
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
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
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public InvoiceManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Invoice.Add(invoice);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(InvoiceUpdateDto invoiceUpdateDto, int id)
    {
        var invoice = _unitOfWork.Invoice.GetInvoiceWithItems(id).Result;
        if(invoiceUpdateDto.Items != null)
        {
            var items = _unitOfWork.Item.GetItemsByInvoiceId(id);
            foreach (var item in items)
            {
                _unitOfWork.Item.Delete(item);
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

        invoice.UpdatedAt = DateTime.Now;
        _unitOfWork.Invoice.Update(invoice);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var invoice = _unitOfWork.Invoice.GetById(id);
        if (invoice==null) return Task.FromResult(0);
        invoice.IsDeleted = true;
        invoice.DeletedAt = DateTime.Now;
        _unitOfWork.Invoice.Update(invoice);
        return _unitOfWork.SaveChangesAsync();
    }

    public InvoiceReadDto? Get(int id)
    {
        var invoice = _unitOfWork.Invoice.GetInvoiceWithItems(id).Result;
        if (invoice == null) return new InvoiceReadDto();
        return new InvoiceReadDto()
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
            Client = _mapper.Map<Client,ClientDto>(invoice.Client),
            Project = _mapper.Map<Project,ProjectDto>(invoice.Project),
        };
    }

    public Task<List<InvoiceReadDto>> GetAll()
    {
        var invoice = _unitOfWork.Invoice.GetAllInvoiceWithItems();
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
        var invoices = await _unitOfWork.Invoice.GetAllInvoiceWithItems();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = invoices.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var invoiceList = invoices.ToList();

            var paginatedResults = invoiceList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedInvoices = new List<InvoiceDto>();
            foreach (var invoice in paginatedResults)
            {
                mappedInvoices.Add(new InvoiceDto()
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
                    ClientId = invoice.Client?.FullName,
                    ProjectId = invoice.Project?.Name,
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

        if (invoices != null)
        {
            IEnumerable<Invoice> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(invoices, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(invoices, column, value2, operator2));
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
                    ClientId = invoice.Client?.FullName,
                    ProjectId = invoice.Project?.Name,
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
    private IEnumerable<Invoice> ApplyFilter(IEnumerable<Invoice> invoices, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => invoices.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => invoices.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => invoices.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => invoices.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var invoiceValue) => ApplyNumericFilter(invoices, column, invoiceValue, operatorType),
            _ => invoices
        };
    }

    private IEnumerable<Invoice> ApplyNumericFilter(IEnumerable<Invoice> invoices, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => invoices.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue == value),
        "neq" => invoices.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue != value),
        "gte" => invoices.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue >= value),
        "gt" => invoices.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue > value),
        "lte" => invoices.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue <= value),
        "lt" => invoices.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var invoiceValue) && invoiceValue < value),
        _ => invoices
    };
}


    public Task<List<InvoiceDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Invoice> invoiceDto;
            invoiceDto = _unitOfWork.Invoice.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var invoice = _mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceDto>>(invoiceDto);
            return Task.FromResult(invoice.ToList());
        }

        var  invoicesDto = _unitOfWork.Invoice.GlobalSearch(searchKey);
        var invoices = _mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceDto>>(invoicesDto);
        return Task.FromResult(invoices.ToList());
    }

}
