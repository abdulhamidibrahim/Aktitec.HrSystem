using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aktitic.HrProject.DAL.Models;

public class Task : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Text { get; set; }=string.Empty;
    
    public string? Description { get; set; }=string.Empty;
    
    public DateTime Date { get; set; }
    
    public string? Priority { get; set; }=string.Empty;

    public bool? Completed { get; set; }
    public TaskList? TaskList { get; set; }=null!;
    
    [ForeignKey(nameof(Models.Project))]
    public int? ProjectId { get; set; }
    
    public Project? Project { get; set; }=null!;
    
    
    [ForeignKey(nameof(AssignEmployee))]
    public int? AssignedTo { get; set; }

    public Employee? AssignEmployee { get; set; }=null!;
    //messages
        
    public IEnumerable<Message>? Messages { get; set; }=null!;
}