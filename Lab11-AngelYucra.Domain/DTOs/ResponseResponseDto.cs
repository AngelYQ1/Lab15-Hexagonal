namespace Lab11_AngelYucra.Domain.DTOs;

public class ResponseResponseDto
{
    public string ResponseId { get; set; } = default!;
    public string TicketId { get; set; } = default!;
    public string ResponderId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
