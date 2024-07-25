using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class TaskList : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ListName { get; set; } = string.Empty;
    public string  Priority { get; set; }
    public DateOnly? DueDate { get; set; }
    public bool? Status { get; set; }
    public int? TaskId { get; set; }
    public Task? Task { get; set; }

    public int TaskBoardId { get; set; }
    public TaskBoard TaskBoard { get; set; }
}