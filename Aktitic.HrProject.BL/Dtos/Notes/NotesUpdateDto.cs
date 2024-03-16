namespace Aktitic.HrProject.BL;

public class NotesUpdateDto
{
    public int? SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public string? Content { get; set; }
    public DateTime? Date { get; set; }
    public bool? Starred { get; set; }
}
