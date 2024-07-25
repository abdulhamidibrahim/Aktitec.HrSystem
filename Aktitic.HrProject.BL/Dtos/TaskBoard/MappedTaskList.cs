using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class MappedTaskList
{
    public int Id { get; set; }
    public string ListName { get; set; } = string.Empty;
    public string  Priority { get; set; }
    public DateOnly? DueDate { get; set; }
    public bool? Status { get; set; }
    public int? TaskId { get; set; }
    public string? TaskText { get; set; }
    public TaskDto? Task { get; set; }

    public int TaskBoardId { get; set; }
}