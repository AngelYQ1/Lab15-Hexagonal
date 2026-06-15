using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Ticket.Commands;

public class DeleteTicketCommand : IRequest<bool>
{
    public string TicketId { get; set; } = default!;
}

internal sealed class DeleteTicketCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteTicketCommand, bool>
{
    public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await unitOfWork.Tickets.GetByIdAsync(request.TicketId, cancellationToken);
        if (ticket is null)
            return false;

        unitOfWork.Tickets.Delete(ticket);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
