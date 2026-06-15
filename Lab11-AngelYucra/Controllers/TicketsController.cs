using Lab11_AngelYucra.Application.UseCases.Ticket.Commands;
using Lab11_AngelYucra.Application.UseCases.Ticket.Queries;
using Lab11_AngelYucra.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_AngelYucra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllTicketsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetTicketByIdQuery { TicketId = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TicketRequestDto request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CreateTicketCommand
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                ClosedAt = request.ClosedAt
            },
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result.TicketId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] TicketRequestDto request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new UpdateTicketCommand
            {
                TicketId = id,
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                ClosedAt = request.ClosedAt
            },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteTicketCommand { TicketId = id }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
