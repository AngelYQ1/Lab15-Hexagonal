using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Role.Queries;

public class GetRoleByIdQuery : IRequest<RoleResponseDto?>
{
    public string RoleId { get; set; } = default!;
}

internal sealed class GetRoleByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoleByIdQuery, RoleResponseDto?>
{
    public async Task<RoleResponseDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.Roles.GetByIdAsync(request.RoleId, cancellationToken);
        return role?.ToDto();
    }
}
