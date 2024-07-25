using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class EventReadDto
{
   public int Id { get; set; }    
   public string Title { get; set; }
   public DateOnly Start { get; set; }
   public DateOnly End { get; set; }
   public string Color { get; set; }
}
