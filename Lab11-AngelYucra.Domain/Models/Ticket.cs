namespace Lab11_AngelYucra.Domain.Models;

public class Ticket
{
    public string TicketId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Response> Responses { get; set; } = new List<Response>();
}