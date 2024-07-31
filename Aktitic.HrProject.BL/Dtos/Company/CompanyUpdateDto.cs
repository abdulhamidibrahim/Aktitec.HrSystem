using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class CompanyUpdateDto
{
    public ApplicationUserDto Manager { get; set; }
    public CompanyDto Company { get; set; }
}
