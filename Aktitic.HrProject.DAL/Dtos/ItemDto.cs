using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Microsoft.VisualBasic.CompilerServices;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class ItemDto
{
    // public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float? Quantity { get; set; }
    public float? UnitCost { get; set; }
    public float? Amount { get; set; }
    // public EstimateDto? Estimate { get; set; }
    // public int? EstimateId { get; set; }
}