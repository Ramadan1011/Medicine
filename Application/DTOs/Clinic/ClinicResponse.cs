using Application.DTOs.Clinic.WorkingHours;

namespace Application.DTOs.Clinic;

public class ClinicResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double Rating { get; set; }

    public int Reviews { get; set; }

    public string? Address { get; set; }

    public string? Landmark { get; set; }

    public List<WorkingHoursResponse>? WorkingHours { get; set; }

    public string? Phone { get; set; }

    public string? Image { get; set; }
}
