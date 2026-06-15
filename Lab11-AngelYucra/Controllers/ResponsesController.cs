using Lab11_AngelYucra.Application.UseCases.Response.Commands;
using Lab11_AngelYucra.Application.UseCases.Response.Queries;
using Lab11_AngelYucra.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_AngelYucra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponsesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllResponsesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetResponseByIdQuery { ResponseId = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ResponseRequestDto request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CreateResponseCommand
            {
                TicketId = request.TicketId,
                ResponderId = request.ResponderId,
                Message = request.Message
            },
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result.ResponseId }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteResponseCommand { ResponseId = id }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
