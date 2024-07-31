using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.BL;

public class LicenseUpdateDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public bool Active { get; set; }
    public int CompanyId { get; set; }
}
