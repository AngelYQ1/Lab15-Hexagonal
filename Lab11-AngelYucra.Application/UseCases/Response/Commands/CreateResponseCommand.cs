using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;
using ResponseEntity = Lab11_AngelYucra.Domain.Models.Response;

namespace Lab11_AngelYucra.Application.UseCases.Response.Commands;

public class CreateResponseCommand : IRequest<ResponseResponseDto>
{
    public string TicketId { get; set; } = default!;
    public string ResponderId { get; set; } = default!;
    public string Message { get; set; } = default!;
}

internal sealed class CreateResponseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateResponseCommand, 
    ResponseResponseDto>
{
    public async Task<ResponseResponseDto> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.Tickets.ExistsAsync(request.TicketId, cancellationToken))
            throw new InvalidOperationException("Ticket does not exist.");

        if (!await unitOfWork.Users.ExistsAsync(request.ResponderId, cancellationToken))
            throw new InvalidOperationException("Responder does not exist.");

        var entity = new ResponseEntity
        {
            ResponseId = Guid.NewGuid().ToString(),
            TicketId = request.TicketId,
            ResponderId = request.ResponderId,
            Message = request.Message.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await unitOfWork.Responses.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.ToDto();
    }
}
