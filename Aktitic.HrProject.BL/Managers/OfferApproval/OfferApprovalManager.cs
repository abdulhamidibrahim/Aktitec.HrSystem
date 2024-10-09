using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class OfferApprovalsManager(
    // UserUtility userUtility,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IOfferApprovalsManager
{
    public Task<int> Add(OfferApprovalAddDto offerApprovalsAddDto)
    {
        var offerApprovals = new OfferApproval
        {
            EmployeeId = offerApprovalsAddDto.EmployeeId,
            JobId = offerApprovalsAddDto.JobId,
            Status = offerApprovalsAddDto.Status,
            Pay = offerApprovalsAddDto.Pay,
            AnnualIp = offerApprovalsAddDto.AnnualIp,
            LongTermIp = offerApprovalsAddDto.LongTermIp,
            // CreatedAt = DateTime.Now,
            // CreatedBy = userUtility.GetUserName(),
        };
        
        unitOfWork.OfferApprovals.Add(offerApprovals);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(OfferApprovalUpdateDto offerApprovalsUpdateDto, int id)
    {
        var offerApprovals = unitOfWork.OfferApprovals.GetById(id);
        
        if (offerApprovals == null) return Task.FromResult(0);

        offerApprovals.EmployeeId = offerApprovalsUpdateDto.EmployeeId;
        offerApprovals.JobId = offerApprovalsUpdateDto.JobId;
        if (offerApprovalsUpdateDto.Pay.IsNullOrEmpty()) 
            offerApprovals.Pay = offerApprovalsUpdateDto.Pay;
        if (offerApprovalsUpdateDto.AnnualIp.IsNullOrEmpty()) 
            offerApprovals.AnnualIp = offerApprovalsUpdateDto.AnnualIp;
        if (offerApprovalsUpdateDto.LongTermIp.IsNullOrEmpty()) 
            offerApprovals.LongTermIp = offerApprovalsUpdateDto.LongTermIp;
        if (offerApprovalsUpdateDto.Status.IsNullOrEmpty()) 
            offerApprovals.Status = offerApprovalsUpdateDto.Status;
      
        
        // offerApprovals.UpdatedAt = DateTime.Now;
        // offerApprovals.UpdatedBy = userUtility.GetUserName();
        
        unitOfWork.OfferApprovals.Update(offerApprovals);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var offerApprovals = unitOfWork.OfferApprovals.GetById(id);
        if (offerApprovals==null) return Task.FromResult(0);
        offerApprovals.IsDeleted = true;
        // offerApprovals.DeletedAt = DateTime.Now;
        // offerApprovals.DeletedBy = userUtility.GetUserName();
        unitOfWork.OfferApprovals.Update(offerApprovals);
        return unitOfWork.SaveChangesAsync();
    }

    public OfferApprovalReadDto? Get(int id)
    {
        var offerApprovals = unitOfWork.OfferApprovals.GetById(id);
        if (offerApprovals == null) return null;
        
        return new OfferApprovalReadDto()
        {
            Id = offerApprovals.Id,
            EmployeeId = offerApprovals.EmployeeId,
            JobId = offerApprovals.JobId,
            Pay = offerApprovals.Pay,
            Status = offerApprovals.Status,
            AnnualIp = offerApprovals.AnnualIp,
            LongTermIp = offerApprovals.LongTermIp,
            CreatedAt = offerApprovals.CreatedAt,
            CreatedBy = offerApprovals.CreatedBy,
            UpdatedAt = offerApprovals.UpdatedAt,
            UpdatedBy = offerApprovals.UpdatedBy,    
        };
    }

    public async Task<List<OfferApprovalReadDto>> GetAll()
    {
        var offerApprovals = await unitOfWork.OfferApprovals.GetAll();
        return offerApprovals.Select(approval => new OfferApprovalReadDto()
        {
            Id = approval.Id,
            EmployeeId = approval.EmployeeId,
            JobId = approval.JobId,
            Pay = approval.Pay,
            Status = approval.Status,
            AnnualIp = approval.AnnualIp,
            LongTermIp = approval.LongTermIp,
            CreatedAt = approval.CreatedAt,
            CreatedBy = approval.CreatedBy,
            UpdatedAt = approval.UpdatedAt,
            UpdatedBy = approval.UpdatedBy,    
            
        }).ToList();
    }

   

    public async Task<FilteredOfferApprovalsDto> GetFilteredOfferApprovalsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var offerApprovalsList = await unitOfWork.OfferApprovals.GetAllOfferApprovals();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = offerApprovalsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = offerApprovalsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var offerApprovalsDto = new List<OfferApprovalDto>();
            foreach (var approval in paginatedResults)
            {
                offerApprovalsDto.Add(new OfferApprovalDto()
                {
                    Id = approval.Id,
                    EmployeeId = approval.EmployeeId,
                    JobId = approval.JobId,
                    Pay = approval.Pay,
                    Status = approval.Status,
                    AnnualIp = approval.AnnualIp,
                    LongTermIp = approval.LongTermIp,
                    Employee = mapper.Map<Employee,EmployeeDto>(approval.Employee),
                    Job = mapper.Map<Job,JobsDto>(approval.Job),
                    CreatedAt = approval.CreatedAt,
                    CreatedBy = approval.CreatedBy,
                    UpdatedAt = approval.UpdatedAt,
                    UpdatedBy = approval.UpdatedBy,    
                });
            }
            FilteredOfferApprovalsDto filteredBudgetsExpensesDto = new()
            {
                OfferApprovalDto = offerApprovalsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (offerApprovalsList != null)
        {
            IEnumerable<OfferApproval> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(offerApprovalsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(offerApprovalsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var offerApprovalss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(offerApprovalss);
            
            var mappedOfferApprovals = new List<OfferApprovalDto>();

            foreach (var approval in paginatedResults)
            {
                
                mappedOfferApprovals.Add(new OfferApprovalDto()
                {
                    Id = approval.Id,
                    EmployeeId = approval.EmployeeId,
                    JobId = approval.JobId,
                    Pay = approval.Pay,
                    Status = approval.Status,
                    AnnualIp = approval.AnnualIp,
                    LongTermIp = approval.LongTermIp,
                    Employee = mapper.Map<Employee,EmployeeDto>(approval.Employee),
                    Job = mapper.Map<Job,JobsDto>(approval.Job),
                    CreatedAt = approval.CreatedAt,
                    CreatedBy = approval.CreatedBy,
                    UpdatedAt = approval.UpdatedAt,
                    UpdatedBy = approval.UpdatedBy,    
                });
            }
            FilteredOfferApprovalsDto filteredOfferApprovalsDto = new()
            {
                OfferApprovalDto = mappedOfferApprovals,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredOfferApprovalsDto;
        }

        return new FilteredOfferApprovalsDto();
    }
    private IEnumerable<OfferApproval> ApplyFilter(IEnumerable<OfferApproval> offerApprovals, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => offerApprovals.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => offerApprovals.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => offerApprovals.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => offerApprovals.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var offerApprovalsValue) => ApplyNumericFilter(offerApprovals, column, offerApprovalsValue, operatorType),
            _ => offerApprovals
        };
    }

    private IEnumerable<OfferApproval> ApplyNumericFilter(IEnumerable<OfferApproval> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var offerApprovalsValue) && offerApprovalsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var offerApprovalsValue) && offerApprovalsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var offerApprovalsValue) && offerApprovalsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var offerApprovalsValue) && offerApprovalsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var offerApprovalsValue) && offerApprovalsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var offerApprovalsValue) && offerApprovalsValue < value),
        _ => policys
    };
}


    public Task<List<OfferApprovalDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<OfferApproval> enumerable = unitOfWork.OfferApprovals.GetAllOfferApprovals().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var offerApprovals = enumerable.Select(approval => new OfferApprovalDto()
            {
                Id = approval.Id,
                EmployeeId = approval.EmployeeId,
                JobId = approval.JobId,
                Pay = approval.Pay,
                Status = approval.Status,
                AnnualIp = approval.AnnualIp,
                LongTermIp = approval.LongTermIp,
                Employee = mapper.Map<Employee,EmployeeDto>(approval.Employee),
                Job = mapper.Map<Job,JobsDto>(approval.Job),
                CreatedAt = approval.CreatedAt,
                CreatedBy = approval.CreatedBy,
                UpdatedAt = approval.UpdatedAt,
                UpdatedBy = approval.UpdatedBy,    
            });
            return Task.FromResult(offerApprovals.ToList());
        }

        var  queryable = unitOfWork.OfferApprovals.GlobalSearch(searchKey);
        var offerApprovalss = queryable.Select(approval => new OfferApprovalDto()
        {
            Id = approval.Id,
            EmployeeId = approval.EmployeeId,
            JobId = approval.JobId,
            Pay = approval.Pay,
            Status = approval.Status,
            AnnualIp = approval.AnnualIp,
            LongTermIp = approval.LongTermIp,
            Employee = mapper.Map<Employee,EmployeeDto>(approval.Employee),
            Job = mapper.Map<Job,JobsDto>(approval.Job),
            CreatedAt = approval.CreatedAt,
            CreatedBy = approval.CreatedBy,
            UpdatedAt = approval.UpdatedAt,
            UpdatedBy = approval.UpdatedBy,    
        });
        return Task.FromResult(offerApprovalss.ToList());
    }

}
