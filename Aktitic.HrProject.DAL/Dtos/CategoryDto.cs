using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class CategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string? SubcategoryName { get; set; }
}