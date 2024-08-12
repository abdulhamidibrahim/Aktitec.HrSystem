﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;


namespace Aktitic.HrProject.BL;

public class OfferApprovalAddDto
{
    public required int EmployeeId { get; set; }
    public required int JobId { get; set; }
    public string? Pay { get; set; }
    public string? AnnualIp { get; set; }
    public string? LongTermIp { get; set; }
    public string? Status { get; set; }

}
