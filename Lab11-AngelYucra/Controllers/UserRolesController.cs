using Lab11_AngelYucra.Application.UseCases.UserRole.Commands;
using Lab11_AngelYucra.Application.UseCases.UserRole.Queries;
using Lab11_AngelYucra.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_AngelYucra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRolesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllUserRolesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{userId}/{roleId}")]
    public async Task<IActionResult> GetById(string userId, string roleId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserRoleByIdQuery { UserId = userId, RoleId = roleId }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserRoleRequestDto request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateUserRoleCommand { UserId = request.UserId, RoleId = request.RoleId }, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { userId = result.UserId, roleId = result.RoleId }, result);
    }

    [HttpDelete("{userId}/{roleId}")]
    public async Task<IActionResult> Delete(string userId, string roleId, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteUserRoleCommand { UserId = userId, RoleId = roleId }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
