using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Ticket.Commands;

public class UpdateTicketCommand : IRequest<TicketResponseDto?>
{
    public string TicketId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string Status { get; set; } = default!;
    public DateTime? ClosedAt { get; set; }
}

internal sealed class UpdateTicketCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateTicketCommand, TicketResponseDto?>
{
    public async Task<TicketResponseDto?> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await unitOfWork.Tickets.GetByIdAsync(request.TicketId, cancellationToken);
        if (ticket is null)
            return null;

        if (!await unitOfWork.Users.ExistsAsync(request.UserId, cancellationToken))
            throw new InvalidOperationException("User does not exist.");

        if (!TicketStatusRules.IsValid(request.Status))
            throw new ArgumentException("Invalid ticket status.");

        ticket.UserId = request.UserId;
        ticket.Title = request.Title.Trim();
        ticket.Description = request.Description?.Trim();
        ticket.Status = request.Status;
        ticket.ClosedAt = request.Status == "cerrado" ? request.ClosedAt ?? DateTime.UtcNow : null;

        unitOfWork.Tickets.Update(ticket);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return ticket.ToDto();
    }
}
