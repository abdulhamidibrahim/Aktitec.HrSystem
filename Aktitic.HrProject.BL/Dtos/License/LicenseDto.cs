namespace Aktitic.HrProject.BL;

public class LicenseDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public bool Active { get; set; }
    public int CompanyId { get; set; }
}