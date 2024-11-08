﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ExpensesAddDto
{
    public string? ItemName { get; set; }
    public string? PurchaseFrom { get; set; }
    public DateOnly? PurchaseDate { get; set; }
    public int? PurchasedById { get; set; }
    // public string? PurchasedBy { get; set; }
    public float? Amount { get; set; }
    public string? PaidBy { get; set; }
    public string? Status { get; set; }
    public ICollection<FileDto> Attachments { get; set; }
}
