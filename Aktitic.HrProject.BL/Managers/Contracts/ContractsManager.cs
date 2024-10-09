using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ContractsManager:IContractsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ContractsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(ContractAddDto contractAddDto)
    {
        var contract = new Contract()
        {
            ContractReference = contractAddDto.ContractReference,
            EmployeeId = contractAddDto.EmployeeId,
            SalaryStructureType = contractAddDto.SalaryStructureType,
            ContractEndDate = contractAddDto.ContractEndDate,
            ContractStartDate = contractAddDto.ContractStartDate,
            DepartmentId = contractAddDto.DepartmentId,
            JobPosition = contractAddDto.JobPosition,
            ContractSchedule = contractAddDto.ContractSchedule,
            ContractType = contractAddDto.ContractType,
            Wage = contractAddDto.Wage,
            Notes = contractAddDto.Notes,
            Status = contractAddDto.Status,
            CreatedAt = DateTime.Now,
        };
        
        _unitOfWork.Contracts.Add(contract);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ContractUpdateDto contractUpdateDto, int id)
    {
        var contract = _unitOfWork.Contracts.GetById(id);
        
        
        if (contract == null) return Task.FromResult(0);
        
        if(contractUpdateDto.ContractReference != null) contract.ContractReference = contractUpdateDto.ContractReference;
        if(contractUpdateDto.EmployeeId != null) contract.EmployeeId = contractUpdateDto.EmployeeId;
        if(contractUpdateDto.SalaryStructureType != null) contract.SalaryStructureType = contractUpdateDto.SalaryStructureType;
        if(contractUpdateDto.ContractEndDate != null) contract.ContractEndDate = contractUpdateDto.ContractEndDate;
        if(contractUpdateDto.ContractStartDate != null) contract.ContractStartDate = contractUpdateDto.ContractStartDate;
        if(contractUpdateDto.DepartmentId != null) contract.DepartmentId = contractUpdateDto.DepartmentId;
        if(contractUpdateDto.JobPosition != null) contract.JobPosition = contractUpdateDto.JobPosition;
        if(contractUpdateDto.ContractSchedule != null) contract.ContractSchedule = contractUpdateDto.ContractSchedule;
        if(contractUpdateDto.ContractType != null) contract.ContractType = contractUpdateDto.ContractType;
        if(contractUpdateDto.Wage != null) contract.Wage = contractUpdateDto.Wage;
        if(contractUpdateDto.Notes != null) contract.Notes = contractUpdateDto.Notes;
        if(contractUpdateDto.Status != null) contract.Status = contractUpdateDto.Status;
       
        //update image
        contract.UpdatedAt = DateTime.Now;
        _unitOfWork.Contracts.Update(contract);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var contract = _unitOfWork.Contracts.GetById(id);
        if (contract==null) return Task.FromResult(0);
        contract.IsDeleted = true;
        contract.DeletedAt = DateTime.Now;
        _unitOfWork.Contracts.Update(contract);
        return _unitOfWork.SaveChangesAsync();
    }

    public ContractReadDto? Get(int id)
    {
        var contract = _unitOfWork.Contracts.GetById(id);
        if (contract == null) return null;
        return new ContractReadDto()
        {
            Id = contract.Id,
            ContractReference = contract.ContractReference,
            EmployeeId = contract.EmployeeId,
            SalaryStructureType = contract.SalaryStructureType,
            ContractEndDate = contract.ContractEndDate,
            ContractStartDate = contract.ContractStartDate,
            DepartmentId = contract.DepartmentId,
            JobPosition = contract.JobPosition,
            ContractSchedule = contract.ContractSchedule,
            ContractType = contract.ContractType,
            Wage = contract.Wage,
            Notes = contract.Notes,
            Status = contract.Status,
        };
    }

    public Task<List<ContractReadDto>> GetAll()
    {
        var contract = _unitOfWork.Contracts.GetAll();
        return Task.FromResult(contract.Result.Select(c=> new ContractReadDto()
        {
            Id = c.Id,
            ContractReference = c.ContractReference,
            EmployeeId = c.EmployeeId,
            SalaryStructureType = c.SalaryStructureType,
            ContractEndDate = c.ContractEndDate,
            ContractStartDate = c.ContractStartDate,
            DepartmentId = c.DepartmentId,
            JobPosition = c.JobPosition,
            ContractSchedule = c.ContractSchedule,
            ContractType = c.ContractType,
            Wage = c.Wage,
            Notes = c.Notes,
            Status = c.Status,

        }).ToList());
    }

     public async Task<FilteredContractDto> GetFilteredContractsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var contracts = await _unitOfWork.Contracts.GetAllWithEmployeeAndDepartment();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = contracts.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var contractList = contracts.ToList();

            var paginatedResults = contractList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = paginatedResults
                .Select(c => new ContractDto()
                {
                    Id = c.Id,
                    ContractReference = c.ContractReference,
                    EmployeeId = c.EmployeeId,
                    SalaryStructureType = c.SalaryStructureType,
                    ContractEndDate = c.ContractEndDate,
                    ContractStartDate = c.ContractStartDate,
                    DepartmentId = c.DepartmentId,
                    JobPosition = c.JobPosition,
                    ContractSchedule = c.ContractSchedule,
                    ContractType = c.ContractType,
                    Wage = c.Wage,
                    Notes = c.Notes,
                    Status = c.Status,
                    Employee = c.Employee.FullName ?? string.Empty,
                    Department = c.Department.Name ?? string.Empty,
                });

            FilteredContractDto result = new()
            {
                ContractDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (contracts != null)
        {
            IEnumerable<Contract> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(contracts, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(contracts, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedContract = paginatedResults
                .Select(c => new ContractDto()
                {
                    Id = c.Id,
                    ContractReference = c.ContractReference,
                    EmployeeId = c.EmployeeId,
                    SalaryStructureType = c.SalaryStructureType,
                    ContractEndDate = c.ContractEndDate,
                    ContractStartDate = c.ContractStartDate,
                    DepartmentId = c.DepartmentId,
                    JobPosition = c.JobPosition,
                    ContractSchedule = c.ContractSchedule,
                    ContractType = c.ContractType,
                    Wage = c.Wage,
                    Notes = c.Notes,
                    Status = c.Status,
                    Employee = c.Employee.FullName ?? string.Empty,
                    Department = c.Department.Name ?? string.Empty,
                });

            FilteredContractDto filteredContractDto = new()
            {
                ContractDto = mappedContract,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredContractDto;
        }

        return new FilteredContractDto();
    }
    private IEnumerable<Contract> ApplyFilter(IEnumerable<Contract> contracts, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => contracts.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => contracts.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => contracts.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => contracts.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var contractValue) => ApplyNumericFilter(contracts, column, contractValue, operatorType),
            _ => contracts
        };
    }

    private IEnumerable<Contract> ApplyNumericFilter(IEnumerable<Contract> contracts, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue == value),
        "neq" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue != value),
        "gte" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue >= value),
        "gt" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue > value),
        "lte" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue <= value),
        "lt" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue < value),
        _ => contracts
    };
}




    public Task<List<ContractDto>> GlobalSearch(string searchKey, string? column)
    {

        if (column != null)
        {
            IEnumerable<Contract> contractDto;
            contractDto = _unitOfWork.Contracts.GetAllWithEmployeeAndDepartment().Result.Where(e =>
                e.GetPropertyValue(column).ToLower().Contains(searchKey, StringComparison.OrdinalIgnoreCase));
            var contract = contractDto.Select(c => new ContractDto()
            {
                Id = c.Id,
                ContractReference = c.ContractReference,
                EmployeeId = c.EmployeeId,
                SalaryStructureType = c.SalaryStructureType,
                ContractEndDate = c.ContractEndDate,
                ContractStartDate = c.ContractStartDate,
                DepartmentId = c.DepartmentId,
                JobPosition = c.JobPosition,
                ContractSchedule = c.ContractSchedule,
                ContractType = c.ContractType,
                Wage = c.Wage,
                Notes = c.Notes,
                Status = c.Status,
                Employee = c.Employee.FullName ?? string.Empty,
                Department = c.Department.Name ?? string.Empty,
            });

        return Task.FromResult(contract.ToList());
        }

        var  contractDtos = _unitOfWork.Contracts.GlobalSearch(searchKey);
        var contracts = contractDtos.Select(c => new ContractDto()
        {
            Id = c.Id,
            ContractReference = c.ContractReference,
            EmployeeId = c.EmployeeId,
            SalaryStructureType = c.SalaryStructureType,
            ContractEndDate = c.ContractEndDate,
            ContractStartDate = c.ContractStartDate,
            DepartmentId = c.DepartmentId,
            JobPosition = c.JobPosition,
            ContractSchedule = c.ContractSchedule,
            ContractType = c.ContractType,
            Wage = c.Wage,
            Notes = c.Notes,
            Status = c.Status,
            Employee = c.Employee.FullName ?? string.Empty,
            Department = c.Department.Name ?? string.Empty,
        });
        return Task.FromResult(contracts.ToList());
    }

}
