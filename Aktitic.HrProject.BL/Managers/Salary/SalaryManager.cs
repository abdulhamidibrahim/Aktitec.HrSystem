
using System.Collections;
using System.Net;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class SalaryManager:ISalaryManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public SalaryManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
       
    }
    
    public Task<int> Add(SalaryAddDto salariesAddDto)
    {
        var salaries = new Salary()
        {
           EmployeeId = salariesAddDto.EmployeeId,
           NetSalary = salariesAddDto.NetSalary,
           Allowance = salariesAddDto.Allowance,
           BasicEarnings = salariesAddDto.BasicEarnings,
           Conveyance = salariesAddDto.Conveyance,
           Da = salariesAddDto.Da,
           Esi = salariesAddDto.Esi,
           Fund = salariesAddDto.Fund,
           Date = salariesAddDto.Date,
           Pf = salariesAddDto.Pf,
           Hra = salariesAddDto.Hra,
           Leave = salariesAddDto.Leave,
           ProfTax = salariesAddDto.ProfTax,
           Tds = salariesAddDto.Tds,
           LabourWelfare = salariesAddDto.LabourWelfare,
           MedicalAllowance = salariesAddDto.MedicalAllowance,
           Others1 = salariesAddDto.Others1,
           Others2 = salariesAddDto.Others2,
           PayslipId = salariesAddDto.PayslipId,
           CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Salary.Add(salaries);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(SalaryUpdateDto salariesUpdateDto, int id)
    {
        var salaries = _unitOfWork.Salary.GetById(id);

        if (salariesUpdateDto.EmployeeId!=null) salaries.EmployeeId = salariesUpdateDto.EmployeeId;
        if (salariesUpdateDto.NetSalary!=null) salaries.NetSalary = salariesUpdateDto.NetSalary;
        if (salariesUpdateDto.Allowance != null) salaries.Allowance = salariesUpdateDto.Allowance;
        if (salariesUpdateDto.Date != null) salaries.Date = salariesUpdateDto.Date;
        if (salariesUpdateDto.BasicEarnings != null) salaries.BasicEarnings = salariesUpdateDto.BasicEarnings;
        if (salariesUpdateDto.Conveyance != null) salaries.Conveyance = salariesUpdateDto.Conveyance;
        if (salariesUpdateDto.Da != null) salaries.Da = salariesUpdateDto.Da;
        if (salariesUpdateDto.Esi != null) salaries.Esi = salariesUpdateDto.Esi;
        if (salariesUpdateDto.Fund != null) salaries.Fund = salariesUpdateDto.Fund;
        if (salariesUpdateDto.Pf != null) salaries.Pf = salariesUpdateDto.Pf;
        if (salariesUpdateDto.Hra != null) salaries.Hra = salariesUpdateDto.Hra;
        if (salariesUpdateDto.Leave != null) salaries.Leave = salariesUpdateDto.Leave;
        if (salariesUpdateDto.ProfTax != null) salaries.ProfTax = salariesUpdateDto.ProfTax;
        if (salariesUpdateDto.Tds != null) salaries.Tds = salariesUpdateDto.Tds;
        if (salariesUpdateDto.LabourWelfare != null) salaries.LabourWelfare = salariesUpdateDto.LabourWelfare;
        if (salariesUpdateDto.MedicalAllowance != null) salaries.MedicalAllowance = salariesUpdateDto.MedicalAllowance;
        if (salariesUpdateDto.Others1 != null) salaries.Others1 = salariesUpdateDto.Others1;
        if (salariesUpdateDto.Others2 != null) salaries.Others2 = salariesUpdateDto.Others2;
        if (salariesUpdateDto.PayslipId != null) salaries.PayslipId = salariesUpdateDto.PayslipId;
        if (salaries == null) return Task.FromResult(0);


        salaries.UpdatedAt = DateTime.Now;
        
        _unitOfWork.Salary.Update(salaries);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var salary = _unitOfWork.Salary.GetById(id);
        if (salary==null) return Task.FromResult(0);
        salary.IsDeleted = true;
        salary.DeletedAt = DateTime.Now;
        _unitOfWork.Salary.Update(salary);
        return _unitOfWork.SaveChangesAsync();
    }

    public SalaryReadDto? Get(int id)
    {
        var salaries = _unitOfWork.Salary.GetWithEmployee(id).FirstOrDefault();
        if (salaries == null) return null;
        
        return new SalaryReadDto()
        {
            Id = salaries.Id,
            EmployeeId = salaries.EmployeeId,
            Employee = _mapper.Map<Employee,EmployeeDto>(salaries.Employee),
            NetSalary = salaries.NetSalary,
            Allowance = salaries.Allowance,
            BasicEarnings = salaries.BasicEarnings,
            Conveyance = salaries.Conveyance,
            Da = salaries.Da,
            Esi = salaries.Esi,
            Fund = salaries.Fund,
            Date = salaries.Date,
            Pf = salaries.Pf,
            Hra = salaries.Hra,
            Leave = salaries.Leave,
            ProfTax = salaries.ProfTax,
            Tds = salaries.Tds,
            LabourWelfare = salaries.LabourWelfare,
            MedicalAllowance = salaries.MedicalAllowance,
            Others1 = salaries.Others1,
            Others2 = salaries.Others2,
            PayslipId = salaries.PayslipId,
        };
    }

    public Task<List<SalaryReadDto>> GetAll()
    {
        var salaries = _unitOfWork.Salary.GetAllWithEmployee();
        return Task.FromResult(salaries.Result.Select(s => new SalaryReadDto()
        {
            Id = s.Id,
            EmployeeId = s.EmployeeId,
            Employee = _mapper.Map<Employee,EmployeeDto>(s.Employee),
            NetSalary = s.NetSalary,
            Allowance = s.Allowance,
            BasicEarnings = s.BasicEarnings,
            Conveyance = s.Conveyance,
            Da = s.Da,
            Esi = s.Esi,
            Fund = s.Fund,
            Date = s.Date,
            Pf = s.Pf,
            Hra = s.Hra,
            Leave = s.Leave,
            ProfTax = s.ProfTax,
            Tds = s.Tds,
            LabourWelfare = s.LabourWelfare,
            MedicalAllowance = s.MedicalAllowance,
            Others1 = s.Others1,
            Others2 = s.Others2,
            PayslipId = s.PayslipId,
        }).ToList());
    }

   

    public async Task<FilteredSalariesDto> GetFilteredSalariesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var salaries = await _unitOfWork.Salary.GetAllWithEmployee();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = salaries.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var salaryList = salaries.ToList();

            var paginatedResults = salaryList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsExpensess = new List<SalaryDto>();
            foreach (var s in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new SalaryDto()
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    Employee = _mapper.Map<Employee,EmployeeDto>(s.Employee),
                    NetSalary = s.NetSalary,
                    Allowance = s.Allowance,
                    BasicEarnings = s.BasicEarnings,
                    Conveyance = s.Conveyance,
                    Da = s.Da,
                    Esi = s.Esi,
                    Fund = s.Fund,
                    Date = s.Date,
                    Pf = s.Pf,
                    Hra = s.Hra,
                    Leave = s.Leave,
                    ProfTax = s.ProfTax,
                    Tds = s.Tds,
                    LabourWelfare = s.LabourWelfare,
                    MedicalAllowance = s.MedicalAllowance,
                    Others1 = s.Others1,
                    Others2 = s.Others2,
                    PayslipId = s.PayslipId,
                });
            }
            FilteredSalariesDto filteredBudgetsExpensesDto = new()
            {
                SalaryDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (salaries != null)
        {
            IEnumerable<Salary> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(salaries, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(salaries, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var salariess = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(salariess);
            
            var mappedSalaries = new List<SalaryDto>();

            foreach (var s in paginatedResults)
            {
                
                mappedSalaries.Add(new SalaryDto()
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    Employee = _mapper.Map<Employee,EmployeeDto>(s.Employee),
                    NetSalary = s.NetSalary,
                    Allowance = s.Allowance,
                    BasicEarnings = s.BasicEarnings,
                    Conveyance = s.Conveyance,
                    Da = s.Da,
                    Esi = s.Esi,
                    Fund = s.Fund,
                    Date = s.Date,
                    Pf = s.Pf,
                    Hra = s.Hra,
                    Leave = s.Leave,
                    ProfTax = s.ProfTax,
                    Tds = s.Tds,
                    LabourWelfare = s.LabourWelfare,
                    MedicalAllowance = s.MedicalAllowance,
                    Others1 = s.Others1,
                    Others2 = s.Others2,
                    PayslipId = s.PayslipId,
                });
            }
            FilteredSalariesDto filteredSalariesDto = new()
            {
                SalaryDto = mappedSalaries,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredSalariesDto;
        }

        return new FilteredSalariesDto();
    }
    private IEnumerable<Salary> ApplyFilter(IEnumerable<Salary> salarys, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => salarys.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => salarys.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => salarys.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => salarys.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var salariesValue) => ApplyNumericFilter(salarys, column, salariesValue, operatorType),
            _ => salarys
        };
    }

    private IEnumerable<Salary> ApplyNumericFilter(IEnumerable<Salary> salarys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => salarys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var salariesValue) && salariesValue == value),
        "neq" => salarys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var salariesValue) && salariesValue != value),
        "gte" => salarys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var salariesValue) && salariesValue >= value),
        "gt" => salarys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var salariesValue) && salariesValue > value),
        "lte" => salarys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var salariesValue) && salariesValue <= value),
        "lt" => salarys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var salariesValue) && salariesValue < value),
        _ => salarys
    };
}


    public Task<List<SalaryDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Salary> salary;
            salary = _unitOfWork.Salary.GetAllWithEmployee().Result.AsEnumerable()
                .Where(e => e.GetPropertyValue(column)
                    .ToLower()
                    .Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var salaries = _mapper.Map<IEnumerable<Salary>, IEnumerable<SalaryDto>>(salary);
            return Task.FromResult(salaries.ToList());
        }

        var  salarys = _unitOfWork.Salary.GlobalSearch(searchKey);
        var salariess = _mapper.Map<IEnumerable<Salary>, IEnumerable<SalaryDto>>(salarys);
        return Task.FromResult(salariess.ToList());
    }

}
