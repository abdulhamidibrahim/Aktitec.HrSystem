using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class DocumentFileUpdateDto
{
    public required IFormFile File { get; set; }
    public string? Description { get; set; }
   
    public required int DocumentId { get; set; }

}
