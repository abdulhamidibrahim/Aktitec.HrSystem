using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class CategoryReadDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string? SubcategoryName { get; set; }
}
