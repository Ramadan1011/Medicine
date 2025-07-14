namespace Application.DTOs.User;

public class UserResponse
{
    public long Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string GoogleId { get; set; } = default!;
    public string? ProfilePictureUrl { get; set; }
}
