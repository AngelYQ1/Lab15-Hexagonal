using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Role.Queries;

public class GetAllRolesQuery : IRequest<IEnumerable<RoleResponseDto>>
{
}

internal sealed class GetAllRolesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleResponseDto>>
{
    public async Task<IEnumerable<RoleResponseDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await unitOfWork.Roles.GetAllAsync(cancellationToken);
        return roles.Select(role => role.ToDto());
    }
}
