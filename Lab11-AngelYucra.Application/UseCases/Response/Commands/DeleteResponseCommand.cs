using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Response.Commands;

public class DeleteResponseCommand : IRequest<bool>
{
    public string ResponseId { get; set; } = default!;
}

internal sealed class DeleteResponseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteResponseCommand, bool>
{
    public async Task<bool> Handle(DeleteResponseCommand request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Responses.GetByIdAsync(request.ResponseId, cancellationToken);
        if (response is null)
            return false;

        unitOfWork.Responses.Delete(response);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
