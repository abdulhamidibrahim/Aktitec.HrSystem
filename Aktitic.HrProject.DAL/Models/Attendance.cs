﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public partial class Attendance
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
