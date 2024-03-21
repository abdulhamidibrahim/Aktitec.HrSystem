using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class LeaveSettingAddDto
{
    public AnnualDto? Annual { get; set; }
    public SickDto? Sick { get; set; }
    public HospitalisationDto? Hospitalisation { get; set; }
    public MaternityDto? Maternity { get; set; }
    public PaternityDto? Paternity { get; set; }
    public LopDto? Lop { get; set; }
}
