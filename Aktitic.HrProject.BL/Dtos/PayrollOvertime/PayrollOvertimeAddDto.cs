using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using File = Aktitic.HrProject.DAL.Models.File;


namespace Aktitic.HrProject.BL;

public class PayrollOvertimeAddDto
{
    public string? Name { get; set; }
    public string? RateType { get; set; }
    public decimal? Rate { get; set; }
}
