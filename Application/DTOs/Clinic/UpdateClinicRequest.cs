using Application.DTOs.Clinic.WorkingHours;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Clinic;

public class UpdateClinicRequest
{
    [Required]
    public long Id { get; set; }

    [MaxLength(200)]
    public string? Name { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Range(0, 5)]
    public double? Rating { get; set; }

    [Range(0, int.MaxValue)]
    public int? Reviews { get; set; }

    [MaxLength(300)]
    public string? Address { get; set; }

    [MaxLength(300)]
    public string? Landmark { get; set; }

    public List<WorkingHoursRequest>? WorkingHours { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(500)]
    public string? Image { get; set; }
}
