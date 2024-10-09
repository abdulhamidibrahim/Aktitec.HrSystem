using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class LogsManager(
    IMapper mapper,
    IUnitOfWork unitOfWork) : ILogsManager
{
   

    public Task<int> Delete(int id)
    {
        unitOfWork.Logs.Delete(id);
        return unitOfWork.SaveChangesAsync();
    }

    public LogsReadDto? Get(int id)
    {
        var logs = unitOfWork.Logs.GetById(id);
        if (logs == null) return null;
        
        return new LogsReadDto()
        {
            Id = logs.Id,
            EntityName = logs.EntityName,
            UserId = logs.UserId,
            Changes = logs.Changes,
            TimeStamp = logs.TimeStamp,
            IpAddress = logs.IpAddress,
            ModifiedRecords = logs.ModifiedRecords,
            TenantId = logs.TenantId,
        };
    }

    public async Task<List<LogsReadDto>> GetAll()
    {
        var logs = await unitOfWork.Logs.GetAllLogs();
        return logs.Select(logs => new LogsReadDto()
        {
            Id = logs.Id,
            EntityName = logs.EntityName,
            UserId = logs.UserId,
            Changes = logs.Changes,
            TimeStamp = logs.TimeStamp,
            IpAddress = logs.IpAddress,
            ModifiedRecords = logs.ModifiedRecords,
            TenantId = logs.TenantId,
        }).ToList();
    }

   

    public async Task<FilteredLogsDto> GetFilteredLogsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var logsList = await unitOfWork.Logs.GetAllLogs();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = logsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = logsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    
            var logsDto = new List<LogsDto>();
            foreach (var logs in paginatedResults)
            {
                logsDto.Add(new LogsDto()
                {
                    Id = logs.Id,
                    EntityName = logs.EntityName,
                    Action = mapper.Map<LogAction,LogActionDto>(logs.Action ?? new LogAction()),
                    UserId = logs.UserId,
                    Changes = logs.Changes,
                    TimeStamp = logs.TimeStamp,
                    IpAddress = logs.IpAddress,
                    AppPages = mapper.Map<AppPages,AppPagesDto>(logs.AppPages ?? new AppPages()),
                    ModifiedRecords = mapper.Map<ICollection<ModifiedRecord>,List<ModifiedRecordDto>>(logs.ModifiedRecords ?? new List<ModifiedRecord>()),
                });
            }
            FilteredLogsDto filteredBudgetsExpensesDto = new()
            {
                LogsDto = logsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (logsList != null)
        {
            IEnumerable<AuditLog> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(logsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(logsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var logss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(logss);
            
            var mappedLogs = new List<LogsDto>();

            foreach (var logs in paginatedResults)
            {
                
                mappedLogs.Add(new LogsDto()
                {
                    Id = logs.Id,
                    EntityName = logs.EntityName,
                    Action = mapper.Map<LogAction,LogActionDto>(logs.Action ?? new LogAction()),
                    UserId = logs.UserId,
                    Changes = logs.Changes,
                    TimeStamp = logs.TimeStamp,
                    IpAddress = logs.IpAddress,
                    AppPages = mapper.Map<AppPages,AppPagesDto>(logs.AppPages ?? new AppPages()),
                    ModifiedRecords = mapper.Map<ICollection<ModifiedRecord>,List<ModifiedRecordDto>>(logs.ModifiedRecords ?? new List<ModifiedRecord>()),
                });
            }
            FilteredLogsDto filteredLogsDto = new()
            {
                LogsDto = mappedLogs,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredLogsDto;
        }

        return new FilteredLogsDto();
    }
    private IEnumerable<AuditLog> ApplyFilter(IEnumerable<AuditLog> logs, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => logs.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => logs.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => logs.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => logs.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var logsValue) => ApplyNumericFilter(logs, column, logsValue, operatorType),
            _ => logs
        };
    }

    private IEnumerable<AuditLog> ApplyNumericFilter(IEnumerable<AuditLog> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var logsValue) && logsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var logsValue) && logsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var logsValue) && logsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var logsValue) && logsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var logsValue) && logsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var logsValue) && logsValue < value),
        _ => policys
    };
}


    public Task<List<LogsDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<AuditLog> enumerable = unitOfWork.Logs.GetAllLogs().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var logs = enumerable.Select(logs => new LogsDto()
            {
                Id = logs.Id,
                EntityName = logs.EntityName,
                Action = mapper.Map<LogAction,LogActionDto>(logs.Action ?? new LogAction()),
                UserId = logs.UserId,
                Changes = logs.Changes,
                TimeStamp = logs.TimeStamp,
                IpAddress = logs.IpAddress,
                AppPages = mapper.Map<AppPages,AppPagesDto>(logs.AppPages ?? new AppPages()),
                ModifiedRecords = mapper.Map<ICollection<ModifiedRecord>,List<ModifiedRecordDto>>(logs.ModifiedRecords ?? new List<ModifiedRecord>()),
            });
            return Task.FromResult(logs.ToList());
        }

        var  queryable = unitOfWork.Logs.GlobalSearch(searchKey);
        var logss = queryable.Select(logs => new LogsDto()
        {
            Id = logs.Id,
            EntityName = logs.EntityName,
            Action = mapper.Map<LogAction,LogActionDto>(logs.Action ?? new LogAction()),
            UserId = logs.UserId,
            Changes = logs.Changes,
            TimeStamp = logs.TimeStamp,
            IpAddress = logs.IpAddress,
            AppPages = mapper.Map<AppPages,AppPagesDto>(logs.AppPages ?? new AppPages()),
            ModifiedRecords = mapper.Map<ICollection<ModifiedRecord>,List<ModifiedRecordDto>>(logs.ModifiedRecords ?? new List<ModifiedRecord>()),
        });
        return Task.FromResult(logss.ToList());
    }

}
