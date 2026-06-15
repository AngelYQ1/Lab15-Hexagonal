using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.User.Commands;

public class DeleteUserCommand : IRequest<bool>
{
    public string UserId { get; set; } = default!;
}

internal sealed class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return false;

        unitOfWork.Users.Delete(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
