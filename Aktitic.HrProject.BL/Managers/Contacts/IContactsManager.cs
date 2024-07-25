using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IContactsManager
{
    public Task<int> Add(ContactAddDto contactAddDto);
    public Task<int> Update(ContactUpdateDto contactUpdateDto, int id);
    public Task<int> Delete(int id);
    public ContactReadDto? Get(int id);
    public Task<List<ContactReadDto>> GetAll();
    public Task<List<ContactReadDto>> GetByType(string type);
    public Task<List<ContactDto>> GlobalSearch(string searchKey,string? column);
  
}