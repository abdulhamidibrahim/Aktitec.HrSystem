using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class TrainingTypeAddDto
{
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool Status { get; set; }
}
