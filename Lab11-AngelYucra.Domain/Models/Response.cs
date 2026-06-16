namespace Lab11_AngelYucra.Domain.Models;

public class Response
{
    public string ResponseId { get; set; } = null!;
    public string TicketId { get; set; } = null!;
    public string ResponderId { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Ticket Ticket { get; set; } = null!;
    public User Responder { get; set; } = null!;
}