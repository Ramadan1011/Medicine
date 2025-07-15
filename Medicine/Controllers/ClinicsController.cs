using Application.DTOs.Clinic;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.Controllers;

[ApiController]
[Route("api/clinics")]
public class ClinicsController(IClinicService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetClinics()
    {
        var clinics = await service.GetAllClinics();
        return Ok(clinics);
    }

    [HttpGet("{clinicId}")]
    public async Task<IActionResult> GetClinicById(long clinicId)
    {
        var clinic = await service.GetClinicById(clinicId);
        return Ok(clinic);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClinic([FromBody] CreateClinicRequest request)
    {
        var clinic = await service.CreateClinic(request);
        return CreatedAtAction(nameof(GetClinicById), new { clinicId = clinic.Id }, clinic);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateClinic([FromBody] UpdateClinicRequest request)
    {
        var clinic = await service.UpdateClinic(request);
        return Ok(clinic);
    }

    [HttpDelete("{clinicId}")]
    public async Task<IActionResult> DeleteClinic(long clinicId)
    {
        await service.DeleteClinic(clinicId);
        return NoContent();
    }
}
