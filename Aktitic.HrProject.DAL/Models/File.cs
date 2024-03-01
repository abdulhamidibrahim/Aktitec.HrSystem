using System;
using System.Collections.Generic;

namespace Aktitic.HrProject.DAL.Models;

public partial class File 
{
    public int Id { get; set; }
    public string? Name { get; set; }
   
    public string? Extension { get; set; }

    public byte[]? Content { get; set; }

    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public int? TicketId { get; set; }
    public Ticket? Ticket { get; set; }

}
