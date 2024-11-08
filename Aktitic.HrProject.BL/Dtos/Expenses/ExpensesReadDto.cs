﻿using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ExpensesReadDto
{
    public int? Id { get; set; }
    public string? ItemName { get; set; }
    public string? PurchaseFrom { get; set; }
    public DateOnly? PurchaseDate { get; set; }
    public int? PurchasedBy { get; set; }
    public float? Amount { get; set; }
    public string? PaidBy { get; set; }
    public string? Status { get; set; }
    public IEnumerable<FileDto> Attachments { get; set; }
}
