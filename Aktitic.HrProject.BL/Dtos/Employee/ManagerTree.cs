using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL.Dtos.Employee;

public class ManagerTree
{
    public EmployeeDto Employee { get; set; }
    public List<ManagerTree> Subordinates { get; set; } = new ();
}