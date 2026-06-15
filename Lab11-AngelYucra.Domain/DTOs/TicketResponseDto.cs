namespace Lab11_AngelYucra.Domain.DTOs;

public class TicketResponseDto
{
    public string TicketId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
}
