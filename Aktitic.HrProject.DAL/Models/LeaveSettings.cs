using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class LeaveSettings :BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    public int AnnualDays { get; set; }
    public bool AnnualCarryForward { get; set; }
    public int AnnualCarryForwardMax { get; set; }
    public bool AnnualEarnedLeave { get; set; }
    public bool AnnualActive { get; set; }
    
    public int SickDays { get; set; }
    public bool SickActive { get; set; }
    
    public int HospitalisationDays { get; set; }
    public bool HospitalisationActive { get; set; }
    
    public int MaternityDays { get; set; }
    public bool MaternityActive { get; set; }
        
    public int PaternityDays { get; set; }
    public bool PaternityActive { get; set; }
    
    public int LopDays { get; set; }
    public bool LopCarryForward { get; set; }
    public int LopCarryForwardMax { get; set; }
    public bool LopEarnedLeave { get; set; }
    public bool LopActive { get; set; }
    
    
}
