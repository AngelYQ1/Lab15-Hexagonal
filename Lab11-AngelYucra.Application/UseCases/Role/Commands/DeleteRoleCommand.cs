using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Role.Commands;

public class DeleteRoleCommand : IRequest<bool>
{
    public string RoleId { get; set; } = default!;
}

internal sealed class DeleteRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.Roles.GetByIdAsync(request.RoleId, cancellationToken);
        if (role is null)
            return false;

        unitOfWork.Roles.Delete(role);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
