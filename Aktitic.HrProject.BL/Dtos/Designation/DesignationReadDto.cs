using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aktitic.HrProject.BL;

public class DesignationReadDto
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public int? DepartmentId { get; set; }

    public DepartmentDto? Department { get; set; }

}
