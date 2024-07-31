using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class NotificationAddDto
{
    
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }

    public bool IsAll { get; set; }
    
    public Priority Priority { get; set; }
    
    public List<int>? Receivers { get; set; }

}