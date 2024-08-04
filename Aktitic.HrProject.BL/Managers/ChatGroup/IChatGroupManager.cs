using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IChatGroupManager
{
    public Task<int> Add(ChatGroupAddDto chatGroupAddDto);
    
    public Task<int> AddGroupUsers(List<ChatGroupUserDto> chatGroupAddDto, int chatGroupId);
    public Task<int> Update(ChatGroupUpdateDto chatGroupUpdateDto, int id);
    public Task<int> Delete(int id);
    public ChatGroupReadDto? Get(int id);
    public Task<List<ChatGroupReadDto>> GetAll(int page, int pageSize);
    public Task<List<MessageDto>> GetMessagesInPrivateChat(int userId1, int userId2, int page, int pageSize);

    public Task<List<MessageReadDto>> GetGroupMessages(int chatGroupId, int page, int pageSize);

}