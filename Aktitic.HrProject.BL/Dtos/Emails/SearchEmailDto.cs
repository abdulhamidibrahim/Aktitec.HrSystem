namespace Aktitic.HrProject.BL;

public class SearchEmailDto
{
    public string SearchKey { get; set; }
    public int? Sender { get; set; }
    public int? Receiver { get; set; }
    public bool? Trash { get; set; }
    public bool? Starred { get; set; }
    public bool? Archived { get; set; }
    public bool? Draft { get; set; }
}
