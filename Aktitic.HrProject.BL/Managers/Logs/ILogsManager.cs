using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface ILogsManager
{
    public Task<int> Delete(int id);
    public LogsReadDto? Get(int id);
    public Task<List<LogsReadDto>> GetAll();
    public Task<FilteredLogsDto> GetFilteredLogsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<LogsDto>> GlobalSearch(string searchKey,string? column);
  
}