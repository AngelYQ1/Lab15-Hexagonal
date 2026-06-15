using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Ticket.Queries;

public class GetAllTicketsQuery : IRequest<IEnumerable<TicketResponseDto>>
{
}

internal sealed class GetAllTicketsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketResponseDto>>
{
    public async Task<IEnumerable<TicketResponseDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await unitOfWork.Tickets.GetAllAsync(cancellationToken);
        return tickets.Select(ticket => ticket.ToDto());
    }
}
