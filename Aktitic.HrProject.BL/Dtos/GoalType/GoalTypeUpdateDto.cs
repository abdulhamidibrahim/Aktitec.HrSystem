﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class GoalTypeUpdateDto
{
    public string Type { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
}