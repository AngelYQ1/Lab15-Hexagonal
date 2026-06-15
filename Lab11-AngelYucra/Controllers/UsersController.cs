using Lab11_AngelYucra.Application.UseCases.User.Commands;
using Lab11_AngelYucra.Application.UseCases.User.Queries;
using Lab11_AngelYucra.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_AngelYucra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllUsersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserByIdQuery { UserId = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserRequestDto request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Password is required." });
        }

        var result = await mediator.Send(
            new CreateUserCommand
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            },
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result.UserId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UserRequestDto request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new UpdateUserCommand
            {
                UserId = id,
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteUserCommand { UserId = id }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
