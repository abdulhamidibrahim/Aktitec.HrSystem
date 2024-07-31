using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class CompanyReadDto
{
    public ApplicationUserDto Manager { get; set; }
    public CompanyDto Company { get; set; }

}
