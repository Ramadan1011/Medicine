using Application.DTOs.User;

namespace Application.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse> GetUserById(long userId);
    Task<UserResponse> GetUserByEmail(string email);
    Task<List<UserResponse>> GetAllUsers();
    Task<UserResponse> UpdateUser(UpdateUserRequest request);
    Task DeleteUser(long userId);
}
