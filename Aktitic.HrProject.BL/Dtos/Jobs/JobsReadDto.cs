using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.BL;

public class JobsReadDto
{
   public int Id { get; set; }
   public required string JobTitle { get; set; }
   public required int DepartmentId { get; set; }
   public string? JobLocation { get; set; }
   public string? NoOfVacancies { get; set; }
   public int Age { get; set; }
   public string? Experience { get; set; }
   public decimal SalaryFrom { get; set; }
   public decimal SalaryTo { get; set; }
   public required string JobType { get; set; }
   public string? Status { get; set; }
   public string? Description { get; set; }
   public DateTime? StartDate { get; set; }
   public DateTime ExpiredDate { get; set; }
   public string? CreatedBy { get; set; }
   public DateTime? CreatedAt { get; set; }
   public string? UpdatedBy { get; set; }
   public DateTime? UpdatedAt { get; set; }
}
