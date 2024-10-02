using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class TaskBoard : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string ListName { get; set; } = string.Empty;
    public string Color { get; set; }
    
    [ForeignKey(nameof(Project))]
    public int? ProjectId { get; set; }

    public Project? Project { get; set; } 
    
    public ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();
    
}