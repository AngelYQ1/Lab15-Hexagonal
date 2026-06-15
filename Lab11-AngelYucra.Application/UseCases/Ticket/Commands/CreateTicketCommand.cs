using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;
using TicketEntity = Lab11_AngelYucra.Domain.Models.Ticket;

namespace Lab11_AngelYucra.Application.UseCases.Ticket.Commands;

public class CreateTicketCommand : IRequest<TicketResponseDto>
{
    public string UserId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string Status { get; set; } = default!;
    public DateTime? ClosedAt { get; set; }
}

internal sealed class CreateTicketCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTicketCommand, TicketResponseDto>
{
    public async Task<TicketResponseDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.Users.ExistsAsync(request.UserId, cancellationToken))
            throw new InvalidOperationException("User does not exist.");

        if (!TicketStatusRules.IsValid(request.Status))
            throw new ArgumentException("Invalid ticket status.");

        var entity = new TicketEntity
        {
            TicketId = Guid.NewGuid().ToString(),
            UserId = request.UserId,
            Title = request.Title.Trim(),
            Description = request.Description?.Trim(),
            Status = request.Status,
            CreatedAt = DateTime.UtcNow,
            ClosedAt = request.Status == "cerrado" ? request.ClosedAt ?? DateTime.UtcNow : null
        };

        await unitOfWork.Tickets.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.ToDto();
    }
}
