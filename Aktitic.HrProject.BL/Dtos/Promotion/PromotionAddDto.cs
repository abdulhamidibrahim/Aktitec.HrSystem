﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;


namespace Aktitic.HrProject.BL;

public class PromotionAddDto
{
    public int? EmployeeId { get; set; }
    public string? PromotionFrom { get; set; }
    public int? PromotionTo { get; set; }
    public DateOnly? Date { get; set; }
   
}
