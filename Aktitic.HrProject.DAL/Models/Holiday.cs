using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public partial class Holiday : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateOnly? Date { get; set; }

    public NotificationSettings NotificationSettings { get; set; }
    
    [ForeignKey(nameof(NotificationSettings))]
    public int NotificationId { get; set; }
}
