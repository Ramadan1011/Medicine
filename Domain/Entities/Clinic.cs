using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Clinic : AuditableEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Range(0, 5)]
    public double Rating { get; set; }

    [Range(0, int.MaxValue)]
    public int Reviews { get; set; }

    [MaxLength(300)]
    public string? Address { get; set; }

    [MaxLength(300)]
    public string? Landmark { get; set; }

    public virtual List<WorkingHours> WorkingHours { get; set; } = [];

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(500)]
    public string? Image { get; set; }
}