using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;
using RoleEntity = Lab11_AngelYucra.Domain.Models.Role;

namespace Lab11_AngelYucra.Application.UseCases.Role.Commands;

public class CreateRoleCommand : IRequest<RoleResponseDto>
{
    public string RoleName { get; set; } = default!;
}

internal sealed class CreateRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateRoleCommand, RoleResponseDto>
{
    public async Task<RoleResponseDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RoleName))
            throw new ArgumentException("Role name is required.");

        var roles = await unitOfWork.Roles.GetAllAsync(cancellationToken);
        if (roles.Any(role => role.RoleName.Equals(request.RoleName, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Role already exists.");

        var entity = new RoleEntity
        {
            RoleId = Guid.NewGuid().ToString(),
            RoleName = request.RoleName.Trim()
        };

        await unitOfWork.Roles.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.ToDto();
    }
}
