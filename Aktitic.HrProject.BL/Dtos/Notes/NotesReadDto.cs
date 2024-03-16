namespace Aktitic.HrProject.BL;

public class NotesReadDto
{
    public int Id { get; set; }
    public int? SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public string? Content { get; set; }
    public DateTime? Date { get; set; }
    public bool? Starred { get; set; }
    
    public string? FullName { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public  string? Email { get; set; }
    public string? ImgUrl { get; set; }
    public int? ImgId { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public byte? Age { get; set; }

    public string? JobPosition { get; set; }

    public DateTime? JoiningDate { get; set; }

    public byte? YearsOfExperience { get; set; }

    public decimal? Salary { get; set; }

}
