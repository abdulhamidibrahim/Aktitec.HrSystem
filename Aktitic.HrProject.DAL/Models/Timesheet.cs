using System;
using System.Collections.Generic;

namespace Aktitic.HrProject.DAL.Models;

public partial class Timesheet
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public int? ProjectId { get; set; }

    public DateTime? Deadline { get; set; }

    public short? AssignedHours { get; set; }

    public short? Hours { get; set; }

    public int? EmployeeId { get; set; }

    public string? Description { get; set; }

    public virtual Employee IdNavigation { get; set; } = null!;
}
