﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;


namespace Aktitic.HrProject.BL;

public class PoliciesAddDto
{
    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? DepartmentId { get; set; }
    public DateOnly? Date { get; set; }
    public IFormFile? File { get; set; }
        
}
