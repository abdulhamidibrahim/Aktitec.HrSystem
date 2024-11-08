﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class RevenuesCreateDto
{
    public int Id { get; set; }
    public string? RevenueTitle { get; set; }
    public float? RevenueAmount { get; set; }
}
