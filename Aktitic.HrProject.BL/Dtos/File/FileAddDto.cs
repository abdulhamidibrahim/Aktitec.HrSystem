using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class FileAddDto
{
    public string UserId { get; set; }
    public string VersionNumber { get; set; } = string.Empty;
    
    public string FileName { get; set; } = string.Empty;
    public string FileSize { get; set; } = string.Empty;
    public Status Status { get; set; }
    public List<FileUsersDto>? FileUsers { get; set; }
    public IFormFile? File { get; set; }
    public string?  ProjectId { get; set; } 
    

}