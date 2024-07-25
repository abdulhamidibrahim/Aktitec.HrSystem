using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.BL;

public class ResignationUpdateDto
{
    public int EmployeeId { get; set; }
    public string? Reason { get; set; }
    public DateOnly? NoticeDate { get; set; }
    public DateOnly? ResignationDate { get; set; }
}
