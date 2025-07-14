using Application.DTOs.Auth;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Medicine.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("google-auth")]
    public async Task<IActionResult> GoogleAuthenticateAsync([FromBody] GoogleAuthRequest request)
    {
        var response = await service.GoogleAuthenticateAsync(request);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return Ok(new { UserId = userId, Email = email });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        var response = await service.RefreshTokenAsync(request);
        return Ok(response);
    }
}
