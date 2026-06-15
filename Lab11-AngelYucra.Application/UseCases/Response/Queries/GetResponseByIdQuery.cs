using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Response.Queries;

public class GetResponseByIdQuery : IRequest<ResponseResponseDto?>
{
    public string ResponseId { get; set; } = default!;
}

internal sealed class GetResponseByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetResponseByIdQuery, ResponseResponseDto?>
{
    public async Task<ResponseResponseDto?> Handle(GetResponseByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Responses.GetByIdAsync(request.ResponseId, cancellationToken);
        return response?.ToDto();
    }
}
