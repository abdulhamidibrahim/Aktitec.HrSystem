﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class ExperienceUpdateDto
{
    public required string ExperienceLevel { get; set; }
    public required bool Status { get; set; }
}
