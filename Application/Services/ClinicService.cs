using Application.DTOs.Clinic;
using Application.DTOs.Clinic.WorkingHours;
using Application.Mappings;
using Application.Services.Interfaces;
using Infrastructure.Persistence;
using Medicine.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal sealed class ClinicService(ApplicationDbContext context) : IClinicService
{
    public async Task<List<ClinicResponse>> GetAllClinics()
    {
        var clinics = await context.Clinics
            .Select(c => new ClinicResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Rating = c.Rating,
                Reviews = c.Reviews,
                Address = c.Address,
                Landmark = c.Landmark,
                Phone = c.Phone,
                Image = c.Image,
                WorkingHours = c.WorkingHours
                   .Select(wh => new WorkingHoursResponse
                   {
                       DayOfWeek = wh.DayOfWeek,
                       OpenTime = wh.OpenTime,
                       CloseTime = wh.CloseTime
                   }).ToList()
            }).ToListAsync()
            ?? throw new NotFoundException("Clinics not found.");

        return clinics;
    }

    public Task<ClinicResponse> GetClinicById(long clinicId)
    {
        var clinic = context.Clinics
            .Where(c => c.Id == clinicId)
            .Select(c => new ClinicResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Rating = c.Rating,
                Reviews = c.Reviews,
                Address = c.Address,
                Landmark = c.Landmark,
                Phone = c.Phone,
                Image = c.Image,
                WorkingHours = c.WorkingHours
                   .Select(wh => new WorkingHoursResponse
                   {
                       DayOfWeek = wh.DayOfWeek,
                       OpenTime = wh.OpenTime,
                       CloseTime = wh.CloseTime
                   }).ToList()
            })
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException("Clinic not found.");

        return clinic!;
    }

    public async Task<ClinicResponse> CreateClinic(CreateClinicRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var clinic = request.ToEntity();

        context.Clinics.Add(clinic);
        await context.SaveChangesAsync();

        return clinic.ToResponse();
    }

    public async Task<ClinicResponse> UpdateClinic(UpdateClinicRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var clinic = await context.Clinics
            .FirstOrDefaultAsync(c => c.Id == request.Id)
            ?? throw new NotFoundException("Clinic not found.");

        clinic.Update(request);
        await context.SaveChangesAsync();

        return clinic.ToResponse();
    }

    public async Task DeleteClinic(long clinicId)
    {
        var clinic = await context.Clinics
            .FirstOrDefaultAsync(c => c.Id == clinicId)
            ?? throw new NotFoundException("Clinic not found.");

        context.Clinics.Remove(clinic);
        await context.SaveChangesAsync();
    }
}
