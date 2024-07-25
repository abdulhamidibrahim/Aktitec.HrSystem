namespace Aktitic.HrProject.DAL.Models;

public class TaskMessage : BaseEntity
{
    public int? TaskId { get; set; }
    public Task? Task { get; set; }
    public int? MessageId { get; set; }
    public Message? Message { get; set; }
}