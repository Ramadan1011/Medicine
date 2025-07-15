using Application.DTOs.Clinic;

namespace Application.Services.Interfaces;

public interface IClinicService
{
    Task<ClinicResponse> GetClinicById(long clinicId);
    Task<List<ClinicResponse>> GetAllClinics();
    Task<ClinicResponse> CreateClinic(CreateClinicRequest request);
    Task<ClinicResponse> UpdateClinic(UpdateClinicRequest request);
    Task DeleteClinic(long clinicId);
}