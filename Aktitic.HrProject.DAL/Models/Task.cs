using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aktitic.HrProject.DAL.Models;

public class Task
{
    public int Id { get; set; }
    
    public string Title { get; set; }=string.Empty;
    
    public string? Description { get; set; }=string.Empty;
    
    public DateOnly? Date { get; set; }
    
    public string? Priority { get; set; }=string.Empty;

    public bool? Completed { get; set; }
    public TaskList? TaskList { get; set; }=null!;
    
    [ForeignKey(nameof(Models.Project))]
    public int? ProjectId { get; set; }
    
    public Project? Project { get; set; }=null!;
    
    
    [ForeignKey(nameof(Models.Employee))]
    public int? AssignedTo { get; set; }

    public Employee? Employee { get; set; }=null!;
    //messages
}