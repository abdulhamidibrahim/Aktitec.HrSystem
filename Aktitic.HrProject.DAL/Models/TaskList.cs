using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class TaskList
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ListName { get; set; } = string.Empty;

    public List<Task> Tasks { get; set; }=new();
}