using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class AnnualDto
{
    public int Days { get; set; }
    public bool CarryForward { get; set; }
    public int CarryForwardMax { get; set; }
    public bool EarnedLeave { get; set; }
    public bool Active { get; set; }
}