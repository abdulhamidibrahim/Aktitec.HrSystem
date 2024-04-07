using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ExpensesCreateDto
{
    public string? ExpenseTitle { get; set; }
  
    public float? ExpenseAmount { get; set; }
   
}
