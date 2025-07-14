using Application.DTOs.User;
using Domain.Entities;

namespace Application.Mappings;

internal static class UserMappings
{
    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            GoogleId = user.GoogleId,
            ProfilePictureUrl = user.ProfilePictureUrl
        };
    }

    public static void Update(this User user, UpdateUserRequest request)
    {
        user.Id = request.Id;
        user.Email = request.Email ?? string.Empty;
        user.FullName = request.FullName ?? string.Empty;
        user.GoogleId = request.GoogleId ?? string.Empty;
        user.ProfilePictureUrl = request.ProfilePictureUrl;
    }
}
