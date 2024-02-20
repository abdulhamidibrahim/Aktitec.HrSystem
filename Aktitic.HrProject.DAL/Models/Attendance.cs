using System;
using System.Collections.Generic;

namespace Aktitic.HrProject.DAL.Models;

public partial class Attendance
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public DateTime? PunchIn { get; set; }

    public DateTime? PunchOut { get; set; }

    public TimeOnly? Production { get; set; }

    public TimeOnly? Break { get; set; }

    public int? OvertimeId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Overtime? Overtime { get; set; }
}
