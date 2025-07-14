using Application.DTOs.User;
using Application.Services.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Medicine.Exceptions;
using Application.Mappings;

namespace Application.Services;

internal sealed class UserService(ApplicationDbContext context) : IUserService
{
    public async Task<List<UserResponse>> GetAllUsers()
    {
        var users = await context.Users
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                GoogleId = u.GoogleId,
                ProfilePictureUrl = u.ProfilePictureUrl
            })
            .ToListAsync();

        return users;
    }

    public async Task<UserResponse> GetUserByEmail(string email)
    {
        if (email is null)
            throw new ArgumentNullException(nameof(email));

        var user = await context.Users
            .Where(u => u.Email == email)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                GoogleId = u.GoogleId,
                ProfilePictureUrl = u.ProfilePictureUrl
            })
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException("User not found.");

        return user;
    }

    public Task<UserResponse> GetUserById(long userId)
    {
        var user = context.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                GoogleId = u.GoogleId,
                ProfilePictureUrl = u.ProfilePictureUrl
            })
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException("User not found.");

        return user!;
    }

    public async Task<UserResponse> UpdateUser(UpdateUserRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == request.Id)
            ?? throw new NotFoundException("User not found.");

        user.Update(request);
        await context.SaveChangesAsync();

        return user.ToResponse();
    }

    public async Task DeleteUser(long userId)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new NotFoundException("User not found.");

        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}
