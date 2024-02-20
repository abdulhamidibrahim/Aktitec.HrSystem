using System;
using System.Collections.Generic;

namespace Aktitic.HrProject.DAL.Models;

public partial class Designation
{
    public int Id { get; set; }

    public int? Name { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}
