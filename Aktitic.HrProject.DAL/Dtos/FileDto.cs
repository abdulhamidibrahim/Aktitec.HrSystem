using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class FileDto
{
      public string? Name { get; set; }
      public string? Extension { get; set; }
      public byte[]? Content { get; set; }
}