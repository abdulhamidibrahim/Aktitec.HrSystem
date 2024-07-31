using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ChatGroupAddDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ChatGroupUserDto> ChatGroupUsers { get; set; }
}
