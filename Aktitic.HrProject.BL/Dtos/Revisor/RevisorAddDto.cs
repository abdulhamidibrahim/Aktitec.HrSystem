using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;


namespace Aktitic.HrProject.BL;

public class RevisorAddDto
{
    
    public int EmployeeId { get; set; }
    
    public int DocumentId { get; set; }
    
    // public string? Notes { get; set; }
    
}
