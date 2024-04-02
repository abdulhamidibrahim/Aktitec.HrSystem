﻿namespace Aktitic.HrProject.BL;

public class AttendanceReadDto
{
    public int Id { get; set; }
    public DateOnly? Date { get; set; }

    public DateTime? PunchIn { get; set; }

    public DateTime? PunchOut { get; set; }

    public string? Production { get; set; }

    public string? Break { get; set; }

    public string? Overtime { get; set; }

    public int? EmployeeId { get; set; }
}
