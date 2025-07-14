using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(GoogleId), IsUnique = true)]
public class User : AuditableEntity
{

    [Required, MaxLength(100), EmailAddress]
    public string Email { get; set; } = default!;

    [Required, MaxLength(100)]
    public string FullName { get; set; } = default!;

    [Required, MaxLength(100)]
    public string GoogleId { get; set; } = default!;

    [MaxLength(300)]
    public string? ProfilePictureUrl { get; set; }
}
