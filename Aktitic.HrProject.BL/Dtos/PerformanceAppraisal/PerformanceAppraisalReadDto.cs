﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class PerformanceAppraisalReadDto
{
    public int Id { get; set; }
    public string? Department { get; set; }
    public string? Designation { get; set; }
    public EmployeeDto? Employee { get; set; }
    public DateOnly? Date { get; set; }
    public bool? Status { get; set; }

}