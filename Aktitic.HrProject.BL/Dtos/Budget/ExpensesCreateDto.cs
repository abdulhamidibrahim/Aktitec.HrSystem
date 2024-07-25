using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ExpensesCreateDto
{
    public int Id { get; set; }
    public string? ExpensesTitle { get; set; }
  
    public float? ExpensesAmount { get; set; }
   
}
