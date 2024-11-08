﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class LicenseReadDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public bool Active { get; set; }
    public int CompanyId { get; set; }
}
