using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public partial class Department : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Designation> Designations { get; set; } = new List<Designation>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Scheduling> Schedulings { get; set; } = new List<Scheduling>();
    
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
    public ICollection<InterviewQuestion>? InterviewQuestions { get; set; }
}
