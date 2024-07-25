using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.BL;

public class PromotionUpdateDto
{
    public int? EmployeeId { get; set; }
    public string? PromotionFrom { get; set; }
    public int? PromotionTo { get; set; }
    public DateOnly? Date { get; set; }
}
