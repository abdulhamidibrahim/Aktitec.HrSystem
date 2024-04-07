﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class TaxAddDto
{
    public string? Name { get; set; }
    public double? Percentage { get; set; }
    public bool? Status { get; set; }
}
