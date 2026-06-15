namespace Lab11_AngelYucra.Domain.DTOs;

public class ResponseRequestDto
{
    public string TicketId { get; set; } = default!;
    public string ResponderId { get; set; } = default!;
    public string Message { get; set; } = default!;
}
