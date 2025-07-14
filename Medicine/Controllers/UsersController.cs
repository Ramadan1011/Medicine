using Application.DTOs.User;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await service.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await service.GetUserByEmail(email);
        return Ok(user);
    }

    [HttpGet("id/{userId}")]
    public async Task<IActionResult> GetUserById(long userId)
    {
        var user = await service.GetUserById(userId);
        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var updatedUser = await service.UpdateUser(request);
        return Ok(updatedUser);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(long userId)
    {
        await service.DeleteUser(userId);
        return NoContent();
    }
}
