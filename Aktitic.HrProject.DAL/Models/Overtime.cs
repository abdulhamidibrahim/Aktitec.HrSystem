using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public partial class Overtime : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateOnly? OtDate { get; set; }

    public byte? OtHours { get; set; }

    public string? OtType { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public int? ApprovedBy { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? ApprovedByNavigation { get; set; }

    // public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Employee? Employee { get; set; }
}
