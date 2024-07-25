using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public partial class File : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
   
    public string? Extension { get; set; }

    public byte[]? Content { get; set; }

    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public int? TicketId { get; set; }
    public Ticket? Ticket { get; set; }

    public int? ExpensesId { get; set; }
    public Expenses? Expenses { get; set; }
}
