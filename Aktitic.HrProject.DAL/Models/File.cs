using System;
using System.Collections.Generic;

namespace Aktitic.HrProject.DAL.Models;

public partial class File
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? EmployeeName { get; set; }

    public string? Extension { get; set; }

    public byte[]? Content { get; set; }

    public int EmployeeId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
