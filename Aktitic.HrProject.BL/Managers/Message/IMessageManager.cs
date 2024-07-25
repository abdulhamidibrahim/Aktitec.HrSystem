using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IMessageManager
{
    public Task<int> Add(MessageAddDto messageAddDto);
    public Task<int> Update(MessageUpdateDto messageUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<MessageReadDto>? Get(int id);
    public Task<List<MessageReadDto>> GetAll();
    // public Task<FilteredMessageDto> GetFilteredMessagesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    // public Task<List<MessageDto>> GlobalSearch(string searchKey,string? column);
  
}