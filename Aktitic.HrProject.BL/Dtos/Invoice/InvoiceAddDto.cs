﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class InvoiceAddDto
{
    
    public string? Email { get; set; }
    public float? Tax { get; set; }
    public string? ClientAddress { get; set; }
    public string? BillingAddress { get; set; }
    public DateOnly? InvoiceDate { get; set; }
    public DateOnly? DueDate { get; set; }
    public string? OtherInformation { get; set; }
    public string? Status { get; set; }
    public string? Notes { get; set; }
    public string? InvoiceNumber { get; set; }
    public float? TotalAmount { get; set; }
    public float? Discount { get; set; }
    public float? GrandTotal { get; set; }
    public int? ClientId { get; set; }
    // public ClientDto? Client { get; set; }
    public int? ProjectId { get; set; }
    // public ProjectDto? Project { get; set; }
    public IEnumerable<ItemDto>? Items { get; set; }

}
