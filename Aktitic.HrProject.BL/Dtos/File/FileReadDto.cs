﻿namespace Aktitic.HrProject.BL;

public class FileReadDto
{
    public int? EmployeeId { get; set; }
    public string? Name { get; set; }
    public string? EmployeeName { get; set; }

    public string? Extension { get; set; }

    public byte[]? Content { get; set; }

}