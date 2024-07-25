namespace Aktitic.HrProject.DAL.Models;

public class Category : BaseEntity
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string? SubcategoryName { get; set; }
}