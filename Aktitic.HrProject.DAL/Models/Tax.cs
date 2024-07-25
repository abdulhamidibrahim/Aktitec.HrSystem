namespace Aktitic.HrProject.DAL.Models;

public class Tax : BaseEntity
{
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Percentage { get; set; }
        public bool? Status { get; set; }
}

