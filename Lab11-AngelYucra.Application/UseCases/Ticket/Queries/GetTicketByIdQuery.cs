using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Ticket.Queries;

public class GetTicketByIdQuery : IRequest<TicketResponseDto?>
{
    public string TicketId { get; set; } = default!;
}

internal sealed class GetTicketByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTicketByIdQuery, TicketResponseDto?>
{
    public async Task<TicketResponseDto?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket = await unitOfWork.Tickets.GetByIdAsync(request.TicketId, cancellationToken);
        return ticket?.ToDto();
    }
}
