using System;
using System.Collections.Generic;

namespace Aktitic.HrProject.DAL.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Designation> Designations { get; set; } = new List<Designation>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Scheduling> Schedulings { get; set; } = new List<Scheduling>();
}
