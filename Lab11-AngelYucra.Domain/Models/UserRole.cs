namespace Lab11_AngelYucra.Domain.Models;

public class UserRole
{
    public string UserId { get; set; } = null!;
    public string RoleId { get; set; } = null!;
    public DateTime AssignedAt { get; set; }
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}