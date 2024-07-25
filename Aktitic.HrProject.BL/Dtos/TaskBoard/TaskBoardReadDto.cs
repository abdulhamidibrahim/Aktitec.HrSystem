namespace Aktitic.HrProject.BL;

public class TaskBoardReadDto
{
    public int Id { get; set; }
    public int? ProjectId { get; set; }
    public string ListName { get; set; }
    public string Color { get; set; }
    
    public List<MappedTaskList> TaskLists { get; set; }
}
