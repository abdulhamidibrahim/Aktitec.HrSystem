using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public partial class Leaves : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string? Type { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string? Reason { get; set; }

    public short? Days { get; set; }
    
    public bool? Approved { get; set; }
    
    public int? ApprovedBy { get; set; }

    public string? Status { get; set; } = string.Empty;
    public virtual Employee? ApprovedByNavigation { get; set; }

    public virtual Employee? Employee { get; set; } 
}
