
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class PerformanceIndicatorManager:IPerformanceIndicatorManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PerformanceIndicatorManager(
                                        IUnitOfWork unitOfWork,
                                        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(PerformanceIndicatorAddDto performanceIndicatorAddDto)
    {
        var performanceIndicator = new PerformanceIndicator()
        {
            DesignationId = performanceIndicatorAddDto.DesignationId,
            DepartmentId = performanceIndicatorAddDto.DepartmentId,
            AddedById = performanceIndicatorAddDto.AddedBy,
            CustomerExperience = performanceIndicatorAddDto.CustomerExperience,
            Management = performanceIndicatorAddDto.Management,
            Administration = performanceIndicatorAddDto.Administration,
            PresentationSkill = performanceIndicatorAddDto.PresentationSkill,
            QualityOfWork = performanceIndicatorAddDto.QualityOfWork,
            Efficiency = performanceIndicatorAddDto.Efficiency,
            Integrity = performanceIndicatorAddDto.Integrity,
            Professionalism = performanceIndicatorAddDto.Professionalism,
            TeamWork = performanceIndicatorAddDto.TeamWork,
            CriticalThinking = performanceIndicatorAddDto.CriticalThinking,
            ConflictManagement = performanceIndicatorAddDto.ConflictManagement,
            Attendance = performanceIndicatorAddDto.Attendance,
            MeetDeadline = performanceIndicatorAddDto.MeetDeadline,
            Marketing = performanceIndicatorAddDto.Marketing,
            Date = performanceIndicatorAddDto.Date,
            Status = performanceIndicatorAddDto.Status,
            CreatedAt = DateTime.Now
        }; 
        _unitOfWork.PerformanceIndicator.Add(performanceIndicator);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(PerformanceIndicatorUpdateDto performanceIndicatorUpdateDto, int id)
    {
        var performanceIndicator = _unitOfWork.PerformanceIndicator.GetById(id);
        
        
        if (performanceIndicator == null) return Task.FromResult(0);
        
        
        if(performanceIndicatorUpdateDto.DesignationId != null) performanceIndicator.DesignationId = performanceIndicatorUpdateDto.DesignationId;
        if(performanceIndicatorUpdateDto.DepartmentId != null) performanceIndicator.DepartmentId = performanceIndicatorUpdateDto.DepartmentId;
        if(performanceIndicatorUpdateDto.AddedBy != null) performanceIndicator.AddedById = performanceIndicatorUpdateDto.AddedBy;
        if(performanceIndicatorUpdateDto.CustomerExperience != null) performanceIndicator.CustomerExperience = performanceIndicatorUpdateDto.CustomerExperience;
        if(performanceIndicatorUpdateDto.Management != null) performanceIndicator.Management = performanceIndicatorUpdateDto.Management;
        if(performanceIndicatorUpdateDto.Administration != null) performanceIndicator.Administration = performanceIndicatorUpdateDto.Administration;
        if(performanceIndicatorUpdateDto.PresentationSkill != null) performanceIndicator.PresentationSkill = performanceIndicatorUpdateDto.PresentationSkill;
        if(performanceIndicatorUpdateDto.QualityOfWork != null) performanceIndicator.QualityOfWork = performanceIndicatorUpdateDto.QualityOfWork;
        if(performanceIndicatorUpdateDto.Efficiency != null) performanceIndicator.Efficiency = performanceIndicatorUpdateDto.Efficiency;
        if(performanceIndicatorUpdateDto.Integrity != null) performanceIndicator.Integrity = performanceIndicatorUpdateDto.Integrity;
        if(performanceIndicatorUpdateDto.Professionalism != null) performanceIndicator.Professionalism = performanceIndicatorUpdateDto.Professionalism;
        if(performanceIndicatorUpdateDto.TeamWork != null) performanceIndicator.TeamWork = performanceIndicatorUpdateDto.TeamWork;
        if(performanceIndicatorUpdateDto.CriticalThinking != null) performanceIndicator.CriticalThinking = performanceIndicatorUpdateDto.CriticalThinking;
        if(performanceIndicatorUpdateDto.ConflictManagement != null) performanceIndicator.ConflictManagement = performanceIndicatorUpdateDto.ConflictManagement;
        if(performanceIndicatorUpdateDto.Attendance != null) performanceIndicator.Attendance = performanceIndicatorUpdateDto.Attendance;
        if(performanceIndicatorUpdateDto.MeetDeadline != null) performanceIndicator.MeetDeadline = performanceIndicatorUpdateDto.MeetDeadline;
        if(performanceIndicatorUpdateDto.Marketing != null) performanceIndicator.Marketing = performanceIndicatorUpdateDto.Marketing;
        if(performanceIndicatorUpdateDto.Date != null) performanceIndicator.Date = performanceIndicatorUpdateDto.Date;
        if(performanceIndicatorUpdateDto.Status != null) performanceIndicator.Status = performanceIndicatorUpdateDto.Status;

        performanceIndicator.UpdatedAt = DateTime.Now;
        
        _unitOfWork.PerformanceIndicator.Update(performanceIndicator);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var performanceIndicator = _unitOfWork.PerformanceIndicator.GetById(id);
        if (performanceIndicator==null) return Task.FromResult(0);
        performanceIndicator.IsDeleted = true;
        performanceIndicator.DeletedAt = DateTime.Now;
        _unitOfWork.PerformanceIndicator.Update(performanceIndicator);
        return _unitOfWork.SaveChangesAsync();
    }

    public PerformanceIndicatorReadSingleDto? Get(int id)
    {
        var indicator = _unitOfWork.PerformanceIndicator.GetWithEmployees(id).FirstOrDefault();
        if (indicator == null) return null;
        return new PerformanceIndicatorReadSingleDto()
        {
            Id = indicator.Id,
            DesignationId = indicator.DesignationId ,
            DepartmentId = indicator.DepartmentId ,
            AddedBy = _mapper.Map<Employee,EmployeeDto>(indicator.AddedBy),
            CustomerExperience = indicator.Marketing,
            Management = indicator.Management,
            Administration = indicator.Administration,
            PresentationSkill = indicator.PresentationSkill,
            QualityOfWork = indicator.QualityOfWork,
            Efficiency = indicator.Efficiency,
            Integrity = indicator.Integrity,
            Professionalism = indicator.Professionalism,
            TeamWork = indicator.TeamWork,
            CriticalThinking = indicator.CriticalThinking,
            ConflictManagement = indicator.ConflictManagement,
            Attendance = indicator.Attendance,
            MeetDeadline = indicator.MeetDeadline,
            Marketing = indicator.Marketing,
            Date = indicator.Date,
            Status = indicator.Status,
        };
    }

    public Task<List<PerformanceIndicatorReadDto>> GetAll()
    {
        var performanceIndicator = _unitOfWork.PerformanceIndicator.GetAllWithEmployees();
        return Task.FromResult(performanceIndicator.Select(indicator => new PerformanceIndicatorReadDto()
        {
            Id = indicator.Id,
            DesignationId = indicator.DesignationId != null ? indicator.Designation!.Name : null,
            DepartmentId = indicator.DepartmentId != null ? indicator.Department!.Name : null,
            AddedBy = _mapper.Map<Employee,EmployeeDto>(indicator.AddedBy),
            CustomerExperience = indicator.Marketing,
            Management = indicator.Management,
            Administration = indicator.Administration,
            PresentationSkill = indicator.PresentationSkill,
            QualityOfWork = indicator.QualityOfWork,
            Efficiency = indicator.Efficiency,
            Integrity = indicator.Integrity,
            Professionalism = indicator.Professionalism,
            TeamWork = indicator.TeamWork,
            CriticalThinking = indicator.CriticalThinking,
            ConflictManagement = indicator.ConflictManagement,
            Attendance = indicator.Attendance,
            MeetDeadline = indicator.MeetDeadline,
            Marketing = indicator.Marketing,
            Date = indicator.Date,
            Status = indicator.Status,
        }).ToList());
    }
    
     public async Task<FilteredPerformanceIndicatorDto> GetFilteredPerformanceIndicatorsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var performanceIndicators =  _unitOfWork.PerformanceIndicator.GetAllWithEmployees();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = performanceIndicators.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var performanceIndicatorList = performanceIndicators.ToList();

            var paginatedResults = performanceIndicatorList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedPerformanceIndicators = new List<PerformanceIndicatorDto>();
            foreach (var indicator in paginatedResults)
            {
                mappedPerformanceIndicators.Add(new PerformanceIndicatorDto()
                {
                    Id = indicator.Id,
                    Designation = indicator.DesignationId != null ? indicator.Designation!.Name : null,
                    Department = indicator.DepartmentId != null ? indicator.Department!.Name : null,
                    AddedBy = _mapper.Map<Employee,EmployeeDto>(indicator.AddedBy),
                    CustomerExperience = indicator.Marketing,
                    Management = indicator.Management,
                    Administration = indicator.Administration,
                    PresentationSkill = indicator.PresentationSkill,
                    QualityOfWork = indicator.QualityOfWork,
                    Efficiency = indicator.Efficiency,
                    Integrity = indicator.Integrity,
                    Professionalism = indicator.Professionalism,
                    TeamWork = indicator.TeamWork,
                    CriticalThinking = indicator.CriticalThinking,
                    ConflictManagement = indicator.ConflictManagement,
                    Attendance = indicator.Attendance,
                    MeetDeadline = indicator.MeetDeadline,
                    Marketing = indicator.Marketing,
                    Date = indicator.Date,
                    Status = indicator.Status,
                });
            }
            FilteredPerformanceIndicatorDto filteredPerformanceIndicatorDto = new()
            {
                PerformanceIndicatorDto = mappedPerformanceIndicators,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredPerformanceIndicatorDto;
        }

        if (performanceIndicators != null)
        {
            IEnumerable<PerformanceIndicator> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(performanceIndicators, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(performanceIndicators, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var performanceIndicators = paginatedResults.ToList();
            // var mappedPerformanceIndicator = _mapper.Map<IEnumerable<PerformanceIndicator>, IEnumerable<PerformanceIndicatorDto>>(performanceIndicators);
            
            var mappedPerformanceIndicators = new List<PerformanceIndicatorDto>();

            foreach (var indicator in paginatedResults)
            {
                
                mappedPerformanceIndicators.Add(new PerformanceIndicatorDto()
                {
                    Id = indicator.Id,
                    Designation = indicator.DesignationId != null ? indicator.Designation!.Name : null,
                    Department = indicator.DepartmentId != null ? indicator.Department!.Name : null,
                    AddedBy = _mapper.Map<Employee,EmployeeDto>(indicator.AddedBy),
                    CustomerExperience = indicator.Marketing,
                    Management = indicator.Management,
                    Administration = indicator.Administration,
                    PresentationSkill = indicator.PresentationSkill,
                    QualityOfWork = indicator.QualityOfWork,
                    Efficiency = indicator.Efficiency,
                    Integrity = indicator.Integrity,
                    Professionalism = indicator.Professionalism,
                    TeamWork = indicator.TeamWork,
                    CriticalThinking = indicator.CriticalThinking,
                    ConflictManagement = indicator.ConflictManagement,
                    Attendance = indicator.Attendance,
                    MeetDeadline = indicator.MeetDeadline,
                    Marketing = indicator.Marketing,
                    Date = indicator.Date,
                    Status = indicator.Status,
                });
            }
            FilteredPerformanceIndicatorDto filteredPerformanceIndicatorDto = new()
            {
                PerformanceIndicatorDto = mappedPerformanceIndicators,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredPerformanceIndicatorDto;
        }

        return new FilteredPerformanceIndicatorDto();
    }
    private IEnumerable<PerformanceIndicator> ApplyFilter(IEnumerable<PerformanceIndicator> performanceIndicators, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => performanceIndicators.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => performanceIndicators.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => performanceIndicators.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => performanceIndicators.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var performanceIndicatorValue) => ApplyNumericFilter(performanceIndicators, column, performanceIndicatorValue, operatorType),
            _ => performanceIndicators
        };
    }

    private IEnumerable<PerformanceIndicator> ApplyNumericFilter(IEnumerable<PerformanceIndicator> performanceIndicators, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => performanceIndicators.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceIndicatorValue) && performanceIndicatorValue == value),
        "neq" => performanceIndicators.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceIndicatorValue) && performanceIndicatorValue != value),
        "gte" => performanceIndicators.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceIndicatorValue) && performanceIndicatorValue >= value),
        "gt" => performanceIndicators.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceIndicatorValue) && performanceIndicatorValue > value),
        "lte" => performanceIndicators.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceIndicatorValue) && performanceIndicatorValue <= value),
        "lt" => performanceIndicators.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceIndicatorValue) && performanceIndicatorValue < value),
        _ => performanceIndicators
    };
}


    public Task<List<PerformanceIndicatorDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<PerformanceIndicator> performanceIndicatorDto;
            performanceIndicatorDto = _unitOfWork.PerformanceIndicator.GetAllWithEmployees().AsEnumerable().Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var performanceIndicator = performanceIndicatorDto.Select(indicator => new PerformanceIndicatorDto()
            {
                Id = indicator.Id,
                Designation = indicator.DesignationId != null ? indicator.Designation!.Name : null,
                Department = indicator.DepartmentId != null ? indicator.Department!.Name : null,
                AddedBy = _mapper.Map<Employee,EmployeeDto>(indicator.AddedBy),
                CustomerExperience = indicator.Marketing,
                Management = indicator.Management,
                Administration = indicator.Administration,
                PresentationSkill = indicator.PresentationSkill,
                QualityOfWork = indicator.QualityOfWork,
                Efficiency = indicator.Efficiency,
                Integrity = indicator.Integrity,
                Professionalism = indicator.Professionalism,
                TeamWork = indicator.TeamWork,
                CriticalThinking = indicator.CriticalThinking,
                ConflictManagement = indicator.ConflictManagement,
                Attendance = indicator.Attendance,
                MeetDeadline = indicator.MeetDeadline,
                Marketing = indicator.Marketing,
                Date = indicator.Date,
                Status = indicator.Status,
            });
            return Task.FromResult(performanceIndicator.ToList());
        }

        var  performanceIndicatorDtos = _unitOfWork.PerformanceIndicator.GlobalSearch(searchKey);
        var performanceIndicators = performanceIndicatorDtos.Select(indicator => new PerformanceIndicatorDto()
        {
            Id = indicator.Id,
            Designation = indicator.DesignationId != null ? indicator.Designation!.Name : null,
            Department = indicator.DepartmentId != null ? indicator.Department!.Name : null,
            AddedBy = _mapper.Map<Employee,EmployeeDto>(indicator.AddedBy),
            CustomerExperience = indicator.Marketing,
            Management = indicator.Management,
            Administration = indicator.Administration,
            PresentationSkill = indicator.PresentationSkill,
            QualityOfWork = indicator.QualityOfWork,
            Efficiency = indicator.Efficiency,
            Integrity = indicator.Integrity,
            Professionalism = indicator.Professionalism,
            TeamWork = indicator.TeamWork,
            CriticalThinking = indicator.CriticalThinking,
            ConflictManagement = indicator.ConflictManagement,
            Attendance = indicator.Attendance,
            MeetDeadline = indicator.MeetDeadline,
            Marketing = indicator.Marketing,
            Date = indicator.Date,
            Status = indicator.Status,
        });
        return Task.FromResult(performanceIndicators.ToList());
    }

}
