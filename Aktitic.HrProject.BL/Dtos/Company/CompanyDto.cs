using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.BL;

public class CompanyDto
{
    public int? Id { get; set; }
    public string CompanyName { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Website { get; set; } = string.Empty;
    public string? Fax { get; set; } = string.Empty;
    public string? Country { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public string? State { get; set; } = string.Empty;
    public string? Postal { get; set; } = string.Empty;
    public string? Contact { get; set; } = string.Empty;
    public string? Logo { get; set; } = string.Empty;
    public string? Language { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }

    public static implicit operator Company(CompanyDto companyDto)
    {
        return new Company()
        {
            CompanyName = companyDto.CompanyName,
            Email = companyDto.Email,
            Address = companyDto.Address,
            Phone = companyDto.Phone,
            Website = companyDto.Website,
            Fax = companyDto.Fax,
            Country = companyDto.Country,
            City = companyDto.City,
            State = companyDto.State,
            Postal = companyDto.Postal,
            Contact = companyDto.Contact,
            Logo = companyDto.Logo,
            Language = companyDto.Language,
        };
    }
}