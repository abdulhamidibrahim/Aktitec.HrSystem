using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class ChatGroupAddDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile? Image { get; set; }
    public string GroupUsers { get; set; }
}
