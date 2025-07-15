using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class WorkingHours : AuditableEntity
{
    public long ClinicId { get; set; }
    public Clinic? Clinic { get; set; }

    [Required]
    public string DayOfWeek { get; set; } = default!;

    [Required]
    public string OpenTime { get; set; } = default!;

    [Required]
    public string CloseTime { get; set; } = default!;
}
