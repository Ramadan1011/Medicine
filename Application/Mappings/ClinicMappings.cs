using Application.DTOs.Clinic;
using Application.DTOs.Clinic.WorkingHours;
using Domain.Entities;

namespace Application.Mappings;

internal static class ClinicMappings
{
    public static Clinic ToEntity(this CreateClinicRequest request)
    {
        return new Clinic
        {
            Name = request.Name,
            Description = request.Description,
            Rating = request.Rating,
            Reviews = request.Reviews,
            Address = request.Address,
            Landmark = request.Landmark,
            Phone = request.Phone,
            Image = request.Image,
            WorkingHours = request.WorkingHours?
                            .Select(wh => new WorkingHours
                            {
                                DayOfWeek = wh.DayOfWeek,
                                OpenTime = wh.OpenTime,
                                CloseTime = wh.CloseTime
                            }).ToList() ?? []
        };
    }

    public static ClinicResponse ToResponse(this Clinic clinic)
    {
        return new ClinicResponse
        {
            Id = clinic.Id,
            Name = clinic.Name,
            Description = clinic.Description,
            Rating = clinic.Rating,
            Reviews = clinic.Reviews,
            Address = clinic.Address,
            Landmark = clinic.Landmark,
            Phone = clinic.Phone,
            Image = clinic.Image,
            WorkingHours = clinic.WorkingHours?
                            .Select(wh => new WorkingHoursResponse
                            {
                                DayOfWeek = wh.DayOfWeek,
                                OpenTime = wh.OpenTime,
                                CloseTime = wh.CloseTime
                            }).ToList() ?? [],
        };
    }

    public static void Update(this Clinic clinic, UpdateClinicRequest request)
    {
        clinic.Name = request.Name ?? clinic.Name;
        clinic.Description = request.Description ?? clinic.Description;
        clinic.Rating = request.Rating ?? clinic.Rating;
        clinic.Reviews = request.Reviews ?? clinic.Reviews;
        clinic.Address = request.Address ?? clinic.Address;
        clinic.Landmark = request.Landmark ?? clinic.Landmark;
        clinic.Phone = request.Phone ?? clinic.Phone;
        clinic.Image = request.Image ?? clinic.Image;

        if (request.WorkingHours is not null)
        {
            clinic.WorkingHours.Clear();
            clinic.WorkingHours = request.WorkingHours
                                    .Select(wh => new WorkingHours
                                    {
                                        DayOfWeek = wh.DayOfWeek,
                                        OpenTime = wh.OpenTime,
                                        CloseTime = wh.CloseTime
                                    }).ToList();
        }
    }
}
