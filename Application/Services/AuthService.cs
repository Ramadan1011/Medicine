using Application.DTOs.Auth;
using Application.Services.Interfaces;
using Domain.Entities;
using Google.Apis.Auth;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Application.Services;

internal sealed class AuthService(
    ApplicationDbContext context,
    IOptions<AuthConfig> options) : IAuthService
{
    public async Task<AuthResponse> GoogleAuthenticateAsync(GoogleAuthRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == payload.Email);

        if (user is null)
        {
            user = new User
            {
                Email = payload.Email,
                FullName = payload.Name ?? "Test",
                GoogleId = payload.Subject,
                ProfilePictureUrl = payload.Picture
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        var accessToken = GenerateAccessToken(user);
        var refreshTokenValue = GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenValue,
            UserId = user.Id,
            ExpirationDate = DateTime.UtcNow.AddDays(options.Value.RefreshTokenExpirationDays)
        };

        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue
        };
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var token = context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefault(rt => rt.Token == request.RefreshToken && !rt.IsRevoked);

        if (token is null || token.ExpirationDate < DateTime.UtcNow)
            throw new InvalidOperationException("Invalid or expired refresh token.");

        var user = token.User;
        if (user is null)
            throw new SecurityTokenException("User not found.");

        var newAccessToken = GenerateAccessToken(user);
        var newRefreshTokenValue = GenerateRefreshToken();

        token.IsRevoked = true;

        context.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshTokenValue,
            UserId = user.Id,
            ExpirationDate = DateTime.UtcNow.AddDays(options.Value.RefreshTokenExpirationDays)
        });

        await context.SaveChangesAsync();

        return new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshTokenValue
        };
    }

    private string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new("fullName", user.FullName)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(options.Value.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(options.Value.AccessTokenExpirationMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
