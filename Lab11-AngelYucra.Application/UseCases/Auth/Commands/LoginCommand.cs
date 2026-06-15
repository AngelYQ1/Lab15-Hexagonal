using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using Lab11_AngelYucra.Domain.Ports.IServices;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.Auth.Commands;

public class LoginCommand : IRequest<LoginResponseDto?>
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}

internal sealed class LoginCommandHandler(IUnitOfWork unitOfWork, IAuthService authService) : IRequestHandler<LoginCommand, LoginResponseDto?>
{
    public async Task<LoginResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.Users.GetAllAsync(cancellationToken);
        var user = users.FirstOrDefault(item =>
            item.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase));

        if (user is null || !authService.VerifyPassword(request.Password, user.PasswordHash))
            return null;

        var userRoles = await unitOfWork.UserRoles.GetAllAsync(cancellationToken);
        var roles = userRoles
            .Where(item => item.UserId == user.UserId)
            .Select(item => item.Role?.RoleName)
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .Cast<string>()
            .ToList();

        return new LoginResponseDto
        {
            Token = authService.GenerateToken(user, roles),
            Username = user.Username,
            Roles = roles
        };
    }
}
