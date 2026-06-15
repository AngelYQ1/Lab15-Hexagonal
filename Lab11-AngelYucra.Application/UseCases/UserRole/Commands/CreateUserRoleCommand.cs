using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;
using UserRoleEntity = Lab11_AngelYucra.Domain.Models.UserRole;

namespace Lab11_AngelYucra.Application.UseCases.UserRole.Commands;

public class CreateUserRoleCommand : IRequest<UserRoleResponseDto>
{
    public string UserId { get; set; } = default!;
    public string RoleId { get; set; } = default!;
}

internal sealed class CreateUserRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserRoleCommand, UserRoleResponseDto>
{
    public async Task<UserRoleResponseDto> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.Users.ExistsAsync(request.UserId, cancellationToken))
            throw new InvalidOperationException("User does not exist.");

        if (!await unitOfWork.Roles.ExistsAsync(request.RoleId, cancellationToken))
            throw new InvalidOperationException("Role does not exist.");

        if (await unitOfWork.UserRoles.ExistsAsync(request.UserId, request.RoleId, cancellationToken))
            throw new InvalidOperationException("User role already exists.");

        var entity = new UserRoleEntity
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            AssignedAt = DateTime.UtcNow
        };

        await unitOfWork.UserRoles.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var created = await unitOfWork.UserRoles.GetByIdAsync(request.UserId, request.RoleId, cancellationToken);
        return (created ?? entity).ToDto();
    }
}
