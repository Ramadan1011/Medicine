namespace Application.DTOs.Auth;

using System.ComponentModel.DataAnnotations;

public class AuthConfig
{
    public const string SectionName = "AuthConfig";

    [Required(ErrorMessage = "SecretKey majburiy")]
    [MinLength(16, ErrorMessage = "SecretKey kamida 16 ta belgidan iborat bo‘lishi kerak")]
    public string SecretKey { get; set; } = default!;

    [Required(ErrorMessage = "Issuer majburiy")]
    public string Issuer { get; set; } = default!;

    [Required(ErrorMessage = "Audience majburiy")]
    public string Audience { get; set; } = default!;

    [Range(30, 1440, ErrorMessage = "AccessToken muddati 30 dan 1440 (24 soat) daqiqagacha bo‘lishi kerak")]
    public int AccessTokenExpirationMinutes { get; set; } = 60;

    [Range(7, 365, ErrorMessage = "RefreshToken muddati 7 dan 365 kungacha bo‘lishi kerak")]
    public int RefreshTokenExpirationDays { get; set; } = 30;
}

