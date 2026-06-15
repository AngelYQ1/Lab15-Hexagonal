namespace Lab11_AngelYucra.Domain.Models;

public class UserRole
{
    public string UserId { get; set; } = default!;
    public string RoleId { get; set; } = default!;
    public DateTime AssignedAt { get; set; }
    public User? User { get; set; }
    public Role? Role { get; set; }
}
