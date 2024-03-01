namespace Aktitic.HrProject.DAL.Models;

public class TaskList
{
    public int Id { get; set; }
    public string ListName { get; set; } = string.Empty;

    public List<Task> Tasks { get; set; }=new();
}