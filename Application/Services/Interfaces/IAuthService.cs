using Application.DTOs.Auth;

namespace Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> GoogleAuthenticateAsync(GoogleAuthRequest request);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
}
