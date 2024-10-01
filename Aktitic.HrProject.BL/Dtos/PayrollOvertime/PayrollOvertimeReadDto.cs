﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class PayrollOvertimeReadDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? RateType { get; set; }
    public decimal? Rate { get; set; }
}
