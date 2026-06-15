namespace Lab11_AngelYucra.Domain.DTOs;

public class UserRoleResponseDto
{
    public string UserId { get; set; } = default!;
    public string RoleId { get; set; } = default!;
    public DateTime AssignedAt { get; set; }
    public string? Username { get; set; }
    public string? RoleName { get; set; }
}
