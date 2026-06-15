namespace Lab11_AngelYucra.Domain.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; } = default!;
    public string Username { get; set; } = default!;
    public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
}
