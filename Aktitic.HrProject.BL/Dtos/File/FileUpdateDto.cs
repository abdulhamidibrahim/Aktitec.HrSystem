using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class FileUpdateDto
{
    public string? FileName { get; set; }
    public string FileSize { get; set; }
    public int UserId { get; set; }
    public Status Status { get; set; }
    public List<FileUsersDto> FileUsers { get; set; }
    public string VersionNumber { get; set; }
    public int ProjectId { get; set; }
    public IFormFile File { get; set; }
}
