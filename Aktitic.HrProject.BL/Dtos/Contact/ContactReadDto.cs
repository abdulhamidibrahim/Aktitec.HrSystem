using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ContactReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public string Role { get; set; }
    public string Type { get; set; }
    public string? Image { get; set; }
    public bool Status { get; set; }
}
