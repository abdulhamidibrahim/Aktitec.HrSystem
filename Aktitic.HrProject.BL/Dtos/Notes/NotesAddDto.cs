namespace Aktitic.HrProject.BL;

public class NotesAddDto
{
    public int? SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public string? Content { get; set; }
    // public DateOnly? Date { get; set; }
    public bool? Starred { get; set; } = false;
}
