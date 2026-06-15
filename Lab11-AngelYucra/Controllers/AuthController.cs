using Lab11_AngelYucra.Application.UseCases.Auth.Commands;
using Lab11_AngelYucra.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_AngelYucra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginCommand
        {
            Username = request.Username,
            Password = request.Password
        }, cancellationToken);
        return result is null ? Unauthorized() : Ok(result);
    }
}
