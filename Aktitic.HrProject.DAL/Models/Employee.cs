using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Models;

// [Table("Employee")]
public partial class Employee 
    // : ApplicationUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public  int Id { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public  string? Email { get; set; }
    public string? ImgUrl { get; set; }
    public int? ImgId { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public byte? Age { get; set; }

    public string? JobPosition { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public byte? YearsOfExperience { get; set; }

    public decimal? Salary { get; set; }

    public string? FileName { get; set; }
    public string? FileContent { get; set; }
    public string? FileExtension { get; set; }
    
    public bool? TeamLeader { get; set; }

    public int? DepartmentId { get; set; }

    public int? ManagerId { get; set; } 

    [ForeignKey(nameof(Project))]
    public int? ProjectId { get; set; }
    public Project? Project { get; set; } 
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Department? Department { get; set; }

    // public virtual UserFile? Img { get; set; }

    public virtual ICollection<Leaves> LeafApprovedByNavigations { get; set; } = new List<Leaves>();

    public virtual ICollection<Leaves>? LeafEmployees { get; set; } = new List<Leaves>();

    public virtual ICollection<Overtime> OvertimeApprovedByNavigations { get; set; } = new List<Overtime>();

    public virtual ICollection<Overtime> OvertimeEmployees { get; set; } = new List<Overtime>();

    public virtual ICollection<Scheduling> SchedulingApprovedByNavigations { get; set; } = new List<Scheduling>();

    public virtual ICollection<Scheduling> SchedulingEmployees { get; set; } = new List<Scheduling>();

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    
    public virtual ICollection<TicketFollowers> TicketFollowers { get; set; } = new List<TicketFollowers>();

    public virtual TimeSheet? Timesheet { get; set; }
    

    
}

// public class ManagerTree
// {
//     public EmployeeDto Employee { get; set; }
//     public List<ManagerTree> Subordinates { get; set; } = new ();
// }
