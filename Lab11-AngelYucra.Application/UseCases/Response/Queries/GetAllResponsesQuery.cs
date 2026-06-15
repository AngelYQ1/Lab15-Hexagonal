using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Response.Queries;

public class GetAllResponsesQuery : IRequest<IEnumerable<ResponseResponseDto>>
{
}

internal sealed class GetAllResponsesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllResponsesQuery, IEnumerable<ResponseResponseDto>>
{
    public async Task<IEnumerable<ResponseResponseDto>> Handle(GetAllResponsesQuery request, CancellationToken cancellationToken)
    {
        var responses = await unitOfWork.Responses.GetAllAsync(cancellationToken);
        return responses.Select(response => response.ToDto());
    }
}
