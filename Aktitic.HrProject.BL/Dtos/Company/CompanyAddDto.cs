using Aktitic.HrProject.DAL.Dtos;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class CompanyAddDto
{
    public UserDto Manager { get; set; }
    public CompanyDto Company { get; set; }
}