namespace Application.DTOs.Clinic.WorkingHours;

public class WorkingHoursRequest
{
    public string DayOfWeek { get; set; } = default!;
    public string OpenTime { get; set; } = default!;
    public string CloseTime { get; set; } = default!;
}
