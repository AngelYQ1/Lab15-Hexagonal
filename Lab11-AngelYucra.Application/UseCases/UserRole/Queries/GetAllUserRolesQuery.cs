using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.UserRole.Queries;

public class GetAllUserRolesQuery : IRequest<IEnumerable<UserRoleResponseDto>>
{
}

internal sealed class GetAllUserRolesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllUserRolesQuery, IEnumerable<UserRoleResponseDto>>
{
    public async Task<IEnumerable<UserRoleResponseDto>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.UserRoles.GetAllAsync(cancellationToken);
        return entities.Select(entity => entity.ToDto());
    }
}
