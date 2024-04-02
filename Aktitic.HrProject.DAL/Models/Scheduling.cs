using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public partial class Scheduling
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? DepartmentId { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? Date { get; set; }

    public int? ShiftId { get; set; }

    public TimeOnly? MinStartTime { get; set; }

    public TimeOnly? MaxStartTime { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? MinEndTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public TimeOnly? MaxEndTime { get; set; }

    public TimeOnly? BreakTime { get; set; }

    public short? RepeatEvery { get; set; }
    
    public bool? ExtraHours { get; set; }
    public bool? Publish { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Employee? Employee { get; set; }
}
