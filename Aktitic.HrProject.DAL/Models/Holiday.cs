using System;
using System.Collections.Generic;

namespace Aktitic.HrProject.DAL.Models;

public partial class Holiday
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateOnly? Date { get; set; }
}
