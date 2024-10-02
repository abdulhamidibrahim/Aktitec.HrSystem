using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.BL;

public class DocumentUsersDto
{
    public int UserId { get; set; }
    public bool? Read { get; set; }
    public bool? Write { get; set; }
}