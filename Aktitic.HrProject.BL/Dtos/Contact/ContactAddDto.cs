using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class ContactAddDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public string Role { get; set; }
    public string? Type { get; set; }
    public IFormFile? Image { get; set; }
    public bool Status { get; set; }
}
