namespace Lab11_AngelYucra.Domain.Models;

public class User
{
    public string UserId { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public ICollection<Response> Responses { get; set; } = new List<Response>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
