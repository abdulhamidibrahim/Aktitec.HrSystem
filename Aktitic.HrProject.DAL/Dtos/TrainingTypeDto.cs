namespace Aktitic.HrProject.DAL.Dtos;

public class TrainingTypeDto
{
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool Status { get; set; }
}  
