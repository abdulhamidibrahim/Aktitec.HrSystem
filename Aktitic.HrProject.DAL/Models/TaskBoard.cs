using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class TaskBoard
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey(nameof(Project))]
    public int? ProjectId { get; set; }

    public Project? Project { get; set; } = null!;
    
}