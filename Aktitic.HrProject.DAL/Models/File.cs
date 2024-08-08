using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public partial class File : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? FileName { get; set; }
   
    public string? FilePath { get; set; }
    public string? FileSize { get; set; }
    public string VersionNumber { get; set; } = string.Empty;

    public Status Status { get; set; }
    
    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }
    public ApplicationUser? User { get; set; } /// <summary>
                                                       /// User who uploaded the file
                                                       /// </summary>

    public List<FileUsers>? FileUsers { get; set; }
    
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public int? TicketId { get; set; }
    public Ticket? Ticket { get; set; }

    public int? ExpensesId { get; set; }
    public Expenses? Expenses { get; set; }
}