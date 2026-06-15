namespace Lab11_AngelYucra.Domain.Models;

public class Response
{
    public string ResponseId { get; set; } = default!;
    public string TicketId { get; set; } = default!;
    public string ResponderId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public Ticket? Ticket { get; set; }
    public User? Responder { get; set; }
}
