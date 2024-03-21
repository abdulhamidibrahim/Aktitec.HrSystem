using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class LeaveSettings
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    // public int Id { get; set; }
    // [ForeignKey(nameof(Annual))]
    // public int AnnualId { get; set; }
    // [ForeignKey(nameof(Sick))]
    // public int SickId { get; set; }
    // [ForeignKey(nameof(Hospitalisation))]
    // public int HospitalisationId { get; set; }
    // [ForeignKey(nameof(Maternity))]
    // public int MaternityId { get; set; }
    // [ForeignKey(nameof(Paternity))]
    // public int PaternityId { get; set; }
    // [ForeignKey(nameof(Lop))]
    // public int LopId { get; set; }
    // public Annual? Annual { get; set; }
    // public Sick? Sick { get; set; }
    // public Hospitalisation? Hospitalisation { get; set; }
    // public Maternity? Maternity { get; set; }
    // public Paternity? Paternity { get; set; }
    // public Lop? Lop { get; set; }
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

// public class Annual 
// {
//     public int Days { get; set; }
//     public bool CarryForward { get; set; }
//     public int CarryForwardMax { get; set; }
//     public bool EarnedLeave { get; set; }
//     public bool Active { get; set; }
// }
//
// public class Sick 
// {
//     public int Days { get; set; }
//     public bool Active { get; set; }
// }
//
//
// public class Hospitalisation 
// {
//     public int Days { get; set; }
//     public bool Active { get; set; }
// }
//
// public class Maternity 
// {
//     public int Days { get; set; }
//     public bool Active { get; set; }
// }
//
// public class Paternity 
// {
//     public int Days { get; set; }
//     public bool Active { get; set; }
// }
//
// public class Lop 
// {
//     public int Days { get; set; }
//     public bool CarryForward { get; set; }
//     public int CarryForwardMax { get; set; }
//     public bool EarnedLeave { get; set; }
//     public bool Active { get; set; }
// }