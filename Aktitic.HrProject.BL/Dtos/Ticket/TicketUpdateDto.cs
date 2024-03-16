﻿namespace Aktitic.HrProject.BL;

public class TicketUpdateDto
{
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Priority { get; set; }
    public string? Status { get; set; } 
    public string? Cc { get; set; }

    public int?  AssignedToEmployeeId { get; set; }

    public int? CreatedByEmployeeId { get; set; }
    
    public int? ClientId { get; set; }
}
