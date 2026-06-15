using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.UserRole.Commands;

public class DeleteUserRoleCommand : IRequest<bool>
{
    public string UserId { get; set; } = default!;
    public string RoleId { get; set; } = default!;
}

internal sealed class DeleteUserRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.UserRoles.GetByIdAsync(request.UserId, request.RoleId, cancellationToken);
        if (entity is null)
            return false;

        unitOfWork.UserRoles.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
