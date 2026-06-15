namespace Lab11_AngelYucra.Domain.DTOs;

public class UserResponseDto
{
    public string UserId { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
}
