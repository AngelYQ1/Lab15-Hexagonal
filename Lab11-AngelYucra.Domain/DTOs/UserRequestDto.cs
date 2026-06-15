namespace Lab11_AngelYucra.Domain.DTOs;

public class UserRequestDto
{
    public string Username { get; set; } = default!;
    public string? Password { get; set; }
    public string? Email { get; set; }
}
