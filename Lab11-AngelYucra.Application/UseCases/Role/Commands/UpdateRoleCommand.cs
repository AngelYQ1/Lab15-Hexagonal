using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Role.Commands;

public class UpdateRoleCommand : IRequest<RoleResponseDto?>
{
    public string RoleId { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}

internal sealed class UpdateRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRoleCommand, RoleResponseDto?>
{
    public async Task<RoleResponseDto?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.Roles.GetByIdAsync(request.RoleId, cancellationToken);
        if (role is null)
            return null;

        var roles = await unitOfWork.Roles.GetAllAsync(cancellationToken);
        if (roles.Any(item => item.RoleId != request.RoleId &&
                              item.RoleName.Equals(request.RoleName, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Role already exists.");

        role.RoleName = request.RoleName.Trim();
        unitOfWork.Roles.Update(role);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return role.ToDto();
    }
}
