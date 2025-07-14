namespace Application.DTOs.User;

public class UpdateUserRequest
{
    public long Id { get; set; } = default!;
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? GoogleId { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
