using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;


namespace Aktitic.HrProject.BL;

public class AptitudeResultsAddDto
{
    public required int EmployeeId { get; set; }
    public required int JobId { get; set; }
    public string? CategoryWiseMark { get; set; }
    public string? TotalMark { get; set; }
    public string? Status { get; set; }
}
