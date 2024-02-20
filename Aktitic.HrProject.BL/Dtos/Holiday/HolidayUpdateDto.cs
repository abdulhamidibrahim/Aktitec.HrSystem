namespace Aktitic.HrProject.BL;

public class HolidayUpdateDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateOnly? Date { get; set; }
}
