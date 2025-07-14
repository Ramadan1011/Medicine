using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Index(nameof(Token), IsUnique = true)]
public class RefreshToken : AuditableEntity
{

    [Required, MaxLength(512)]
    public string Token { get; set; } = default!;

    [Required]
    public DateTime ExpirationDate { get; set; }

    [Required]
    public bool IsRevoked { get; set; } = false;

    [Required, ForeignKey(nameof(User))]
    public long UserId { get; set; }

    public User User { get; set; } = default!;
}
