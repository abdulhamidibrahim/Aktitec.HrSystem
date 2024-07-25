
using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class PayrollOvertimeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? RateType { get; set; }
    public decimal? Rate { get; set; }
}