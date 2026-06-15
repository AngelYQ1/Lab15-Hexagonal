using Lab11_AngelYucra.Application.UseCases.Role.Commands;
using Lab11_AngelYucra.Application.UseCases.Role.Queries;
using Lab11_AngelYucra.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_AngelYucra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllRolesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetRoleByIdQuery { RoleId = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoleRequestDto request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateRoleCommand { RoleName = request.RoleName }, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.RoleId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] RoleRequestDto request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateRoleCommand { RoleId = id, RoleName = request.RoleName }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteRoleCommand { RoleId = id }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
