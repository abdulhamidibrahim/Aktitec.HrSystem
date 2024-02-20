using System;
using System.Collections.Generic;

namespace Aktitic.HrProject.DAL.Models;

public partial class Leaves
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string? Type { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string? Reason { get; set; }

    public short? Days { get; set; }

    public bool? Approved { get; set; }

    public int? ApprovedBy { get; set; }

    public virtual Employee? ApprovedByNavigation { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
