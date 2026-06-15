using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.UserRole.Queries;

public class GetUserRoleByIdQuery : IRequest<UserRoleResponseDto?>
{
    public string UserId { get; set; } = default!;
    public string RoleId { get; set; } = default!;
}

internal sealed class GetUserRoleByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserRoleByIdQuery, UserRoleResponseDto?>
{
    public async Task<UserRoleResponseDto?> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.UserRoles.GetByIdAsync(request.UserId, request.RoleId, cancellationToken);
        return entity?.ToDto();
    }
}
